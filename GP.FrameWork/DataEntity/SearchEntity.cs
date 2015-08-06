using GP.FrameWork.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GP.FrameWork.DataEntity
{
    public class SearchEntity : DataBase
    {
        public List<SearchItem> Collection { get; private set; }
        public List<string> SearchItems { get; private set; }
        public List<string[]> Orders { get; private set; }

        public string SearchID { get; set; }

        public string ExtensionCondition { get; set; }

        public SearchEntity()
        {
            Collection = new List<SearchItem>();
        }

        public SearchEntity(string searchID)
        {
            this.SearchID = searchID;
            Collection = new List<SearchItem>();
        }

        public void AddSearch(params string[] value)
        {
            if (SearchItems == null)
            {
                SearchItems = new List<string>();
            }
            SearchItems.AddRange(value);
        }

        public void OrderBy(string key, EnumOrderBy by = EnumOrderBy.Asc)
        {
            if (Orders == null)
            {
                Orders = new List<string[]>();
            }
            Orders.Add(new[] { key, by == EnumOrderBy.Desc ? "desc" : "asc" });
        }

        public void Clear()
        {
            Collection.Clear();
            if (SearchItems != null && SearchItems.Count > 0)
            {
                SearchItems.Clear();
            }
            if (Orders != null && Orders.Count > 0)
            {
                Orders.Clear();
            }
            ExtensionCondition = string.Empty;
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        protected override EntityItem GetItem(string key)
        {
            foreach (SearchItem item in Collection)
            {
                if (item.Key.Equals(key,StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        public override void SetUIValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullUIValue(key, value))
                {
                    Collection.Remove(item as SearchItem);
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                SearchAttribute sa = this.GetType().GetAttribute<SearchAttribute>(key);
                if (sa != null)
                {
                    if (sa.Operator == SearchOperator.IsNull || sa.Operator == SearchOperator.IsNotNull)
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                            Operator = sa.Operator,
                            Key = key
                        });
                    }
                    else
                    {
                        if (!IsNullUIValue(key, value))
                        {
                            Collection.Add(new SearchItem()
                            {
                                Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                                Operator = sa.Operator,
                                Key = key,
                                Value = value
                            });
                        }
                    }
                }
                else
                {
                    if (!IsNullUIValue(key, value))
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = key,
                            Operator = SearchOperator.Equal,
                            Key = key,
                            Value = value
                        });
                    }
                }
            }
        }

        protected override void SetValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullValue(key, value))
                {
                    Collection.Remove(item as SearchItem);
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                SearchAttribute sa = this.GetType().GetAttribute<SearchAttribute>(key);
                if (sa != null)
                {
                    if (sa.Operator == SearchOperator.IsNull || sa.Operator == SearchOperator.IsNotNull)
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                            Operator = sa.Operator,
                            Key = key
                        });
                    }
                    else
                    {
                        if (!IsNullValue(key, value))
                        {
                            Collection.Add(new SearchItem()
                            {
                                Field = (sa.Field == null || sa.Field.Length == 0) ? key : sa.Field,
                                Operator = sa.Operator,
                                Key = key,
                                Value = value
                            });
                        }
                    }
                }
                else
                {
                    if (!IsNullValue(key, value))
                    {
                        Collection.Add(new SearchItem()
                        {
                            Field = key,
                            Operator = SearchOperator.Equal,
                            Key = key,
                            Value = value
                        });
                    }
                }
            }
        }

        public override void Remove_G()
        {
            for (int i = Collection.Count - 1; i >= 0; --i)
            {
                if (Collection[i].Key.EndsWith("_g",StringComparison.OrdinalIgnoreCase))
                {
                    Collection.RemoveAt(i);
                }
            }
        }
    }

    public class ProcEntity : DataBase
    {
        public List<EntityItem> Collection { get; private set; }

        public ProcEntity()
        {
            Collection = new List<EntityItem>();
        }

        public ProcEntity(string procName)
        {
            ProcName = procName;
            Collection = new List<EntityItem>();
        }

        public string ProcName { get; set; }

        protected override EntityItem GetItem(string key)
        {
            foreach (EntityItem item in Collection)
            {
                if (item.Key.Equals(key,StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        protected override bool IsNullUIValue(string key, object value)
        {
            if (value.ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        public override void SetUIValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullUIValue(key, value))
                {
                    item.Value = DBNull.Value;
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                if (IsNullUIValue(key, value))
                {
                    value = DBNull.Value;
                }
                Collection.Add(new EntityItem() { Key = key, Value = value });
            }
        }

        protected override void SetValue(string key, object value)
        {
            EntityItem item = GetItem(key);
            if (item != null)
            {
                if (IsNullValue(key, value))
                {
                    item.Value = DBNull.Value;
                }
                else
                {
                    item.Value = value;
                }
            }
            else
            {
                if (IsNullValue(key, value))
                {
                    value = DBNull.Value;
                }
                Collection.Add(new EntityItem() { Key = key, Value = value });
            }
        }

        public override void Remove_G()
        {
            for (int i = Collection.Count - 1; i >= 0; --i)
            {
                if (Collection[i].Key.EndsWith("_g"))
                {
                    Collection.RemoveAt(i);
                }
            }
        }
    }
}
