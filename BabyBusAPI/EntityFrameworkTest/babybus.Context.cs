﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityFrameworkTest
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class babybus_testEntities : DbContext
    {
        public babybus_testEntities()
            : base("name=babybus_testEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AnalysisKindergarten> AnalysisKindergarten { get; set; }
        public virtual DbSet<AnalysisParent> AnalysisParent { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<CardPassword> CardPassword { get; set; }
        public virtual DbSet<Checkout> Checkout { get; set; }
        public virtual DbSet<Child> Child { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Favorite> Favorite { get; set; }
        public virtual DbSet<Kindergarten> Kindergarten { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<NoticeChildRelation> NoticeChildRelation { get; set; }
        public virtual DbSet<NoticeReaded> NoticeReaded { get; set; }
        public virtual DbSet<ParentChildRelation> ParentChildRelation { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WeChatInfo> WeChatInfo { get; set; }
        public virtual DbSet<NoticeSummary> NoticeSummary { get; set; }
        public virtual DbSet<SelectUserInformationView> SelectUserInformationView { get; set; }
        public virtual DbSet<SelectUserInformationViewWithId> SelectUserInformationViewWithId { get; set; }
        public virtual DbSet<TeacherInUseSummary> TeacherInUseSummary { get; set; }
    
        public virtual ObjectResult<sp_changeChildKindergartenAndClass_Result> sp_changeChildKindergartenAndClass(string childName, string kindergartenName, string className)
        {
            var childNameParameter = childName != null ?
                new ObjectParameter("childName", childName) :
                new ObjectParameter("childName", typeof(string));
    
            var kindergartenNameParameter = kindergartenName != null ?
                new ObjectParameter("kindergartenName", kindergartenName) :
                new ObjectParameter("kindergartenName", typeof(string));
    
            var classNameParameter = className != null ?
                new ObjectParameter("className", className) :
                new ObjectParameter("className", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_changeChildKindergartenAndClass_Result>("sp_changeChildKindergartenAndClass", childNameParameter, kindergartenNameParameter, classNameParameter);
        }
    
        public virtual ObjectResult<sp_changeChildKindergartenAndClassByChildId_Result> sp_changeChildKindergartenAndClassByChildId(string childId, string kindergartenName, string className)
        {
            var childIdParameter = childId != null ?
                new ObjectParameter("childId", childId) :
                new ObjectParameter("childId", typeof(string));
    
            var kindergartenNameParameter = kindergartenName != null ?
                new ObjectParameter("kindergartenName", kindergartenName) :
                new ObjectParameter("kindergartenName", typeof(string));
    
            var classNameParameter = className != null ?
                new ObjectParameter("className", className) :
                new ObjectParameter("className", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_changeChildKindergartenAndClassByChildId_Result>("sp_changeChildKindergartenAndClassByChildId", childIdParameter, kindergartenNameParameter, classNameParameter);
        }
    
        public virtual ObjectResult<sp_updateAnalysisKindergartenTable_Result> sp_updateAnalysisKindergartenTable()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_updateAnalysisKindergartenTable_Result>("sp_updateAnalysisKindergartenTable");
        }
    
        public virtual ObjectResult<sp_updateAnalysisParentTable_Result> sp_updateAnalysisParentTable()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_updateAnalysisParentTable_Result>("sp_updateAnalysisParentTable");
        }
    }
}
