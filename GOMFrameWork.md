#GOMFrameWorkʹ��˵�� 
	1��EntityBase����Ӧ���ݿ�������ʵ�����
	2��SearchEntity������ִ�����ݲ�ѯ�Ļ���
	3��ProcEntity������ִ�д洢���̵Ļ��ࣨ�������ʹ��SqlProcEntity/OracleProcEntity��
	4��ʹ��ʾ����
		4.1������ʵ�壨�����������޸ģ�
			[ModelBinder(typeof(EntityModelBinder))]
			public class MyEntity : EntityBase /* ʵ�壬��Ӧ���ݿ��еı� */
			{
				/* һЩʵ������ԣ���Ӧ���ݿ����ֶ� */
			}

			--Controller�е�ĳ��Action
			public ActionResult SaveMyEntity(MyEntity entity) /* ����entity�Ѱ���ǰ̨�ύ������ */
			{
				entity.Save();
			}
		4.2����ѯ����
			[ModelBinder(typeof(EntityModelBinder))]
			public class MySearchEntity : EntityBase /* ʵ�壬��Ӧ���ݿ��еı� */
			{
				/* һЩʵ������ԣ���Ӧ���ݿ����ֶ� */
			}

			--Controller�е�ĳ��Action
			public ActionResult SaveMyEntity(MySearchEntity entity)
			{
				entity.SearchID="TableName/ViewName";

				entity.Load<T>(); //��ѯ�����б�
				entity.LoadEntity<T>(); //��ѯһ��ʵ��
			}
		4.3��ִ�д洢����
			--Controller�е�ĳ��Action
			public ActionResult SaveMyEntity(SqlProcEntity entity)
			{
				entity.ProcName="ProcName";

				entity.Execute(); //ִ��һ�������޷���ֵ���򷵻�ResultID(�ɹ�/ʧ��)��Message
				entity.Execute<T>(); //���������б�
				entity.ExecuteData<T>(); //���ظ������ݶ���
			}

#G.Util���ݽ���

##1��G.Util.Account�����ռ��£�����ʵ��Ϊ��¼��������Token���ǳ�����ȡ��ǰ��¼�û�
##2��G.Util.Extension�����ռ��£�
		2.1��EntityExtension.cs�ļ���ʵ�ַ��͵�EntityList������ʵ��ͬʱ����������ݡ�
		2.2��EnumExtension.cs�ļ��ж�ö�����ͽ�����չ�����ڸ���ö������ǰ�˵�������
		2.3��ProcEntity.cs�ļ�����Ҫ��Oracleִ�д洢��������չ�����ڸ��ݷ���Cursor����ʼ��Ϊ�����б�������ݶ���
		2.4��XmlExtension.cs�ļ��ж�XML����չ���ɶ�ȡXMLΪ����ʵ�壬Ҳ�ɱ���ʵ�嵽XML��
##3��G.Util.Html�����ռ��£�ʵ����ǰ�������������β��������Դ
##4��G.Util.Mvc�����ռ��£���Ҫʵ����ʵ�幹���������Mvc���JsonResult�Ŀ������л�����¼Ȩ����֤��
##5��G.Util.Redis�����ռ��£�ʵ�ֶ�Redis�����Ӧ��
##6��G.Util.Tool�����ռ��£�ʵ�ּ��ܡ�Excel�ļ��Ķ�ȡ��������Excel��ͼƬ��ˮӡ����������ͼ��Json���л�������log4net����־��
			�ʼ����͡���ҳ�ؼ����ɡ��õ����ظ�������������顢������֤��������֤��
			