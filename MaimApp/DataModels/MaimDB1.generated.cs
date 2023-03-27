//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : db_a96b40_maimf
	/// Data Source    : SQL8005.site4now.net
	/// Server Version : 16.00.1000
	/// </summary>
	public partial class DbA96b40MaimfDB : LinqToDB.Data.DataConnection
	{
		public ITable<Approval>            Approvals            { get { return this.GetTable<Approval>(); } }
		public ITable<ApprovalRequest>     ApprovalRequests     { get { return this.GetTable<ApprovalRequest>(); } }
		public ITable<BascetLine>          BascetLines          { get { return this.GetTable<BascetLine>(); } }
		public ITable<Basket>              Baskets              { get { return this.GetTable<Basket>(); } }
		public ITable<City>                Cities               { get { return this.GetTable<City>(); } }
		public ITable<County>              Counties             { get { return this.GetTable<County>(); } }
		public ITable<Link>                Links                { get { return this.GetTable<Link>(); } }
		public ITable<PersonDataInRequest> PersonDataInRequests { get { return this.GetTable<PersonDataInRequest>(); } }
		public ITable<Product>             Products             { get { return this.GetTable<Product>(); } }
		public ITable<ProductCategory>     ProductCategories    { get { return this.GetTable<ProductCategory>(); } }
		public ITable<ProductComment>      ProductComments      { get { return this.GetTable<ProductComment>(); } }
		public ITable<Region>              Regions              { get { return this.GetTable<Region>(); } }
		public ITable<Request>             Requests             { get { return this.GetTable<Request>(); } }
		public ITable<Role>                Roles                { get { return this.GetTable<Role>(); } }
		public ITable<User>                Users                { get { return this.GetTable<User>(); } }
		public ITable<UserFavProduct>      UserFavProducts      { get { return this.GetTable<UserFavProduct>(); } }
		public ITable<UserPrData>          UserPrData           { get { return this.GetTable<UserPrData>(); } }

		public DbA96b40MaimfDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DbA96b40MaimfDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DbA96b40MaimfDB(LinqToDBConnectionOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public DbA96b40MaimfDB(LinqToDBConnectionOptions<DbA96b40MaimfDB> options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Approval")]
	public partial class Approval
	{
		[Column(),                      PrimaryKey, Identity] public int Id                { get; set; } // int
		[Column(),                      NotNull             ] public int IsOk              { get; set; } // int
		[Column("Approval_request_id"), NotNull             ] public int ApprovalRequestId { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__Approval__Approv__5DCAEF64 (dbo.Approval_request)
		/// </summary>
		[Association(ThisKey="ApprovalRequestId", OtherKey="Id", CanBeNull=false)]
		public ApprovalRequest ApprovalRequest { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Approval_request")]
	public partial class ApprovalRequest
	{
		[PrimaryKey, Identity] public int      Id     { get; set; } // int
		[Column,     NotNull ] public int      UserId { get; set; } // int
		[Column,     NotNull ] public DateTime Date   { get; set; } // date

		#region Associations

		/// <summary>
		/// FK__Approval__Approv__5DCAEF64_BackReference (dbo.Approval)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ApprovalRequestId", CanBeNull=true)]
		public IEnumerable<Approval> ApprovalApprov5Dcaefs { get; set; }

		/// <summary>
		/// FK__Approval___UserI__5AEE82B9 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="BascetLine")]
	public partial class BascetLine
	{
		[PrimaryKey, Identity] public int Id        { get; set; } // int
		[Column,     NotNull ] public int BasketId  { get; set; } // int
		[Column,     NotNull ] public int ProductId { get; set; } // int
		[Column,     NotNull ] public int Amount    { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__BascetLin__Baske__4AB81AF0 (dbo.Basket)
		/// </summary>
		[Association(ThisKey="BasketId", OtherKey="Id", CanBeNull=false)]
		public Basket Basket { get; set; }

		/// <summary>
		/// FK__BascetLin__Produ__4BAC3F29 (dbo.Product)
		/// </summary>
		[Association(ThisKey="ProductId", OtherKey="Id", CanBeNull=false)]
		public Product Product { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Basket")]
	public partial class Basket
	{
		[PrimaryKey, Identity] public int      Id      { get; set; } // int
		[Column,     NotNull ] public int      UserId  { get; set; } // int
		[Column,     NotNull ] public DateTime DateIns { get; set; } // date

		#region Associations

		/// <summary>
		/// FK__BascetLin__Baske__4AB81AF0_BackReference (dbo.BascetLine)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="BasketId", CanBeNull=true)]
		public IEnumerable<BascetLine> BascetLinBaske4AB81Afs { get; set; }

		/// <summary>
		/// FK__Basket__UserId__47DBAE45 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="City")]
	public partial class City
	{
		[PrimaryKey, Identity] public int    Id       { get; set; } // int
		[Column,     NotNull ] public string Name     { get; set; } // nvarchar(60)
		[Column,     NotNull ] public int    RegionId { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__City__RegionId__656C112C (dbo.Region)
		/// </summary>
		[Association(ThisKey="RegionId", OtherKey="Id", CanBeNull=false)]
		public Region Region { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="County")]
	public partial class County
	{
		[PrimaryKey, Identity] public int    Id   { get; set; } // int
		[Column,     NotNull ] public string Name { get; set; } // nvarchar(60)

		#region Associations

		/// <summary>
		/// FK__Region__CountyId__628FA481_BackReference (dbo.Region)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="CountyId", CanBeNull=true)]
		public IEnumerable<Region> RegionCountyId628Fas { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Link")]
	public partial class Link
	{
		[Column(),       PrimaryKey, Identity] public int    Id         { get; set; } // int
		[Column(),       NotNull             ] public string Name       { get; set; } // nvarchar(60)
		[Column(),       NotNull             ] public int    Number     { get; set; } // int
		[Column("Link"), NotNull             ] public string LinkColumn { get; set; } // nvarchar(max)
	}

	[Table(Schema="dbo", Name="PersonDataInRequest")]
	public partial class PersonDataInRequest
	{
		[PrimaryKey, Identity   ] public int    Id           { get; set; } // int
		[Column,        Nullable] public int?   RequestId    { get; set; } // int
		[Column,     NotNull    ] public bool   Who          { get; set; } // bit
		[Column,     NotNull    ] public bool   Document     { get; set; } // bit
		[Column,     NotNull    ] public string Name         { get; set; } // nvarchar(60)
		[Column,     NotNull    ] public string LastName     { get; set; } // nvarchar(60)
		[Column,     NotNull    ] public int    TelNumber    { get; set; } // int
		[Column,     NotNull    ] public string Mail         { get; set; } // nvarchar(60)
		[Column,     NotNull    ] public int    SerialNumber { get; set; } // int
		[Column,        Nullable] public string HotelAdress  { get; set; } // nvarchar(150)
		[Column,        Nullable] public string HotelName    { get; set; } // nvarchar(150)

		#region Associations

		/// <summary>
		/// FK__PersonDat__Reque__5629CD9C (dbo.Request)
		/// </summary>
		[Association(ThisKey="RequestId", OtherKey="Id", CanBeNull=true)]
		public Request Request { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Product")]
	public partial class Product
	{
		[PrimaryKey, Identity] public int     Id              { get; set; } // int
		[Column,     NotNull ] public char    Name            { get; set; } // nvarchar(1)
		[Column,     NotNull ] public char    Description     { get; set; } // nvarchar(1)
		[Column,     NotNull ] public char    ShorDescription { get; set; } // nvarchar(1)
		[Column,     NotNull ] public decimal Price           { get; set; } // decimal(18, 2)
		[Column,     NotNull ] public int     CategoriId      { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__BascetLin__Produ__4BAC3F29_BackReference (dbo.BascetLine)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ProductId", CanBeNull=true)]
		public IEnumerable<BascetLine> BascetLinProdu4BAC3F { get; set; }

		/// <summary>
		/// FK__Product__Categor__412EB0B6 (dbo.ProductCategory)
		/// </summary>
		[Association(ThisKey="CategoriId", OtherKey="Id", CanBeNull=false)]
		public ProductCategory Categori { get; set; }

		/// <summary>
		/// FK__ProductCo__Produ__44FF419A_BackReference (dbo.ProductComment)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ProductId", CanBeNull=true)]
		public IEnumerable<ProductComment> ProductCoProdu44FF419A { get; set; }

		/// <summary>
		/// FK__Request__Product__534D60F1_BackReference (dbo.Request)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ProductId", CanBeNull=true)]
		public IEnumerable<Request> Request534D60F { get; set; }

		/// <summary>
		/// FK__UserFavPr__Produ__4F7CD00D_BackReference (dbo.UserFavProduct)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="ProductId", CanBeNull=true)]
		public IEnumerable<UserFavProduct> UserFavPrProdu4F7CD00D { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="ProductCategory")]
	public partial class ProductCategory
	{
		[PrimaryKey, Identity] public int    Id   { get; set; } // int
		[Column,     NotNull ] public string Name { get; set; } // nvarchar(45)

		#region Associations

		/// <summary>
		/// FK__Product__Categor__412EB0B6_BackReference (dbo.Product)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="CategoriId", CanBeNull=true)]
		public IEnumerable<Product> ProductCategor412EB0B { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="ProductComment")]
	public partial class ProductComment
	{
		[PrimaryKey, Identity] public int    Id        { get; set; } // int
		[Column,     NotNull ] public int    UserId    { get; set; } // int
		[Column,     NotNull ] public int    ProductId { get; set; } // int
		[Column,     NotNull ] public string Comment   { get; set; } // nvarchar(400)

		#region Associations

		/// <summary>
		/// FK__ProductCo__Produ__44FF419A (dbo.Product)
		/// </summary>
		[Association(ThisKey="ProductId", OtherKey="Id", CanBeNull=false)]
		public Product Product { get; set; }

		/// <summary>
		/// FK__ProductCo__UserI__440B1D61 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Region")]
	public partial class Region
	{
		[PrimaryKey, Identity] public int    Id       { get; set; } // int
		[Column,     NotNull ] public string Name     { get; set; } // nvarchar(60)
		[Column,     NotNull ] public int    CountyId { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__City__RegionId__656C112C_BackReference (dbo.City)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="RegionId", CanBeNull=true)]
		public IEnumerable<City> CityRegionId656C112C { get; set; }

		/// <summary>
		/// FK__Region__CountyId__628FA481 (dbo.County)
		/// </summary>
		[Association(ThisKey="CountyId", OtherKey="Id", CanBeNull=false)]
		public County County { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Request")]
	public partial class Request
	{
		[PrimaryKey, Identity   ] public int      Id        { get; set; } // int
		[Column,     NotNull    ] public int      UserId    { get; set; } // int
		[Column,     NotNull    ] public int      ProductId { get; set; } // int
		[Column,     NotNull    ] public DateTime Date      { get; set; } // date
		[Column,     NotNull    ] public int      People    { get; set; } // int
		[Column,        Nullable] public int?     Child     { get; set; } // int
		[Column,        Nullable] public int?     Adult     { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__PersonDat__Reque__5629CD9C_BackReference (dbo.PersonDataInRequest)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="RequestId", CanBeNull=true)]
		public IEnumerable<PersonDataInRequest> PersonDatReque5629CD9C { get; set; }

		/// <summary>
		/// FK__Request__Product__534D60F1 (dbo.Product)
		/// </summary>
		[Association(ThisKey="ProductId", OtherKey="Id", CanBeNull=false)]
		public Product Product { get; set; }

		/// <summary>
		/// FK__Request__UserId__52593CB8 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Role")]
	public partial class Role
	{
		[PrimaryKey, Identity] public int    Id   { get; set; } // int
		[Column,     NotNull ] public string Name { get; set; } // nvarchar(45)

		#region Associations

		/// <summary>
		/// FK__User__RoleId__398D8EEE_BackReference (dbo.User)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="RoleId", CanBeNull=true)]
		public IEnumerable<User> UserRoleId398D8Eees { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="User")]
	public partial class User
	{
		[PrimaryKey, Identity   ] public int      Id      { get; set; } // int
		[Column,     NotNull    ] public string   Mail    { get; set; } // nvarchar(60)
		[Column,     NotNull    ] public DateTime DateReg { get; set; } // date
		[Column,        Nullable] public int?     RoleId  { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__Approval___UserI__5AEE82B9_BackReference (dbo.Approval_request)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public IEnumerable<ApprovalRequest> ApprovalUserI5AEE82B { get; set; }

		/// <summary>
		/// FK__Basket__UserId__47DBAE45_BackReference (dbo.Basket)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public IEnumerable<Basket> BasketUserId47Dbaes { get; set; }

		/// <summary>
		/// FK__ProductCo__UserI__440B1D61_BackReference (dbo.ProductComment)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public IEnumerable<ProductComment> ProductCoUserI440B1D { get; set; }

		/// <summary>
		/// FK__Request__UserId__52593CB8_BackReference (dbo.Request)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public IEnumerable<Request> RequestUserId52593Cbs { get; set; }

		/// <summary>
		/// FK__User__RoleId__398D8EEE (dbo.Role)
		/// </summary>
		[Association(ThisKey="RoleId", OtherKey="Id", CanBeNull=true)]
		public Role Role { get; set; }

		/// <summary>
		/// FK__UserFavPr__UserI__4E88ABD4_BackReference (dbo.UserFavProduct)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public IEnumerable<UserFavProduct> UserFavPrUserI4E88Abds { get; set; }

		/// <summary>
		/// FK__UserPrDat__UserI__3C69FB99_BackReference (dbo.UserPrData)
		/// </summary>
		[Association(ThisKey="Id", OtherKey="UserId", CanBeNull=true)]
		public UserPrData UserPrDatUserI3C69FB { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="UserFavProduct")]
	public partial class UserFavProduct
	{
		[Column(),           PrimaryKey, Identity] public int      Id        { get; set; } // int
		[Column("Date_add"), NotNull             ] public DateTime DateAdd   { get; set; } // date
		[Column(),           NotNull             ] public int      UserId    { get; set; } // int
		[Column(),           NotNull             ] public int      ProductId { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__UserFavPr__Produ__4F7CD00D (dbo.Product)
		/// </summary>
		[Association(ThisKey="ProductId", OtherKey="Id", CanBeNull=false)]
		public Product Product { get; set; }

		/// <summary>
		/// FK__UserFavPr__UserI__4E88ABD4 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="UserPrData")]
	public partial class UserPrData
	{
		[PrimaryKey, NotNull    ] public int    UserId    { get; set; } // int
		[Column,     NotNull    ] public string Name      { get; set; } // nvarchar(60)
		[Column,     NotNull    ] public string LastName  { get; set; } // nvarchar(60)
		[Column,        Nullable] public string Surname   { get; set; } // nvarchar(60)
		[Column,        Nullable] public int?   TelNumber { get; set; } // int

		#region Associations

		/// <summary>
		/// FK__UserPrDat__UserI__3C69FB99 (dbo.User)
		/// </summary>
		[Association(ThisKey="UserId", OtherKey="Id", CanBeNull=false)]
		public User User { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Approval Find(this ITable<Approval> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ApprovalRequest Find(this ITable<ApprovalRequest> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static BascetLine Find(this ITable<BascetLine> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Basket Find(this ITable<Basket> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static City Find(this ITable<City> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static County Find(this ITable<County> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Link Find(this ITable<Link> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static PersonDataInRequest Find(this ITable<PersonDataInRequest> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Product Find(this ITable<Product> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ProductCategory Find(this ITable<ProductCategory> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static ProductComment Find(this ITable<ProductComment> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Region Find(this ITable<Region> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Request Find(this ITable<Request> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static Role Find(this ITable<Role> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static User Find(this ITable<User> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UserFavProduct Find(this ITable<UserFavProduct> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static UserPrData Find(this ITable<UserPrData> table, int UserId)
		{
			return table.FirstOrDefault(t =>
				t.UserId == UserId);
		}
	}
}
