#GOMFrameWork使用说明 
	
##1、EntityBase：对应数据库表的数据实体基类
##2、SearchEntity：用于执行数据查询的基类
##3、ProcEntity：用于执行存储过程的基类（具体可以使用SqlProcEntity/OracleProcEntity）
##4、使用示例：
		4.1、保存实体（包括新增与修改）
			[ModelBinder(typeof(EntityModelBinder))]
			public class MyEntity : EntityBase /* 实体，对应数据库中的表 */
			{
				/* 一些实体的属性，对应数据库表的字段 */
			}

			--Controller中的某个Action
			public ActionResult SaveMyEntity(MyEntity entity) /* 参数entity已包含前台提交的数据 */
			{
				entity.Save();
			}
		4.2、查询数据
			[ModelBinder(typeof(EntityModelBinder))]
			public class MySearchEntity : EntityBase /* 实体，对应数据库中的表 */
			{
				/* 一些实体的属性，对应数据库表的字段 */
			}

			--Controller中的某个Action
			public ActionResult SaveMyEntity(MySearchEntity entity)
			{
				entity.SearchID="TableName/ViewName";

				entity.Load<T>(); //查询数据列表
				entity.LoadEntity<T>(); //查询一个实体
			}
		4.3、执行存储过程
			--Controller中的某个Action
			public ActionResult SaveMyEntity(SqlProcEntity entity)
			{
				entity.ProcName="ProcName";

				entity.Execute(); //执行一个操作无返回值，或返回ResultID(成功/失败)、Message
				entity.Execute<T>(); //返回数据列表
				entity.ExecuteData<T>(); //返回复杂数据对象
			}

#G.Util内容介绍

##1、G.Util.Account命名空间下，用于实现为登录用户设置Token、登出、获取当前登录用户
##2、G.Util.Extension命名空间下，
		2.1、EntityExtension.cs文件中实现泛型的EntityList，用于实现同时保存多条数据。
		2.2、EnumExtension.cs文件中对枚举类型进行扩展，用于根据枚举生成前端的下拉框。
		2.3、ProcEntity.cs文件中主要对Oracle数据库执行存储过程做扩展，用于根据返回的Cursor，初始化为数据列表或复杂数据对象。
		2.4、XmlExtension.cs文件中对XML做扩展，可读取XML为数据实体，也可保存实体到XML。
##3、G.Util.Html命名空间下，实现了前端下拉框与树形插件的数据源
##4、G.Util.Mvc命名空间下，主要实现了实体构造器，针对Mvc框架JsonResult的快速序列化，登录权限认证。
##5、G.Util.Redis命名空间下，实现对Redis缓存的应用
##6、G.Util.Tool命名空间下，实现加密、Excel文件的读取、导出到Excel、图片加水印、生成缩略图、Json序列化、基于log4net的日志、
			邮件发送、分页控件生成、得到不重复的随机整数数组、数据验证、生成验证码
			