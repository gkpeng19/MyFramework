
using GP.FrameWork.DataEntity;
using GP.FrameWork.DBHelper;
using System;
using System.Collections;
using System.Text;
using System.Transactions;

namespace GP.FrameWork.Extension
{
    public static class EntityExtension
    {
        /// <summary>
        /// 新增、修改实体，连同Details共同保存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static long Save(this EntityProvider provider, EntityBase entity)
        {
            long id = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                provider.BeginTransaction();

                if (entity.ID > 0)
                {
                    id = entity.ID;
                    provider.ExcuteUpdate(entity);
                }
                else
                {
                    id = provider.ExcuteInsert(entity).ResultID;
                }

                object obj = entity.GetEntityDetails();
                if (obj != null)
                {
                    var list = obj as IList;
                    foreach (EntityBase data in list)
                    {
                        if (data.ID > 0)
                        {
                            if (data.GetEntityStatus() == EntityStatus.Delete)
                            {
                                provider.ExcuteDelete(data);
                            }
                            else
                            {
                                provider.ExcuteUpdate(data);
                            }
                        }
                        else
                        {
                            string fkey = entity.TableInfo.FKey;
                            if (fkey != null && fkey.Length > 0)
                            {
                                data[fkey] = entity.ID;
                            }
                            provider.ExcuteInsert(data);
                        }
                    }
                }
                scope.Complete();
            }
            return id;
        }

        public static void Delete(this EntityProvider provider, EntityBase entity)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                provider.BeginTransaction();

                object obj = entity.GetEntityDetails();
                if (obj != null)
                {
                    var list = obj as IList;
                    foreach (EntityBase data in list)
                    {
                        provider.ExcuteDelete(data);
                    }
                }
                provider.ExcuteDelete(entity);

                scope.Complete();
            }
        }
    }
}
