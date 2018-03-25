
create table [eduq].[Gender]
(
	Id int identity(1,1) primary key,
	[Name] varchar(75),
	StateId int
);
alter table eduq.[Gender] add constraint [FK_eduq.Gender_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);

create table [eduq].[AppUser]
(
	Id uniqueidentifier primary key,
	[Name] varchar(150),
	FirstName varchar(75),
	LastName varchar(75),
	MiddleName varchar(75),
	GenderId int,
	BirthDay Date,
	EmailAddress varchar(50),
	ContactNumber varchar(13),
	CreatedOn DateTime,
	CreatedById uniqueidentifier,
	ModifiedOn DateTime,
	ModifiedById uniqueidentifier,
	StateId int
);
alter table eduq.[AppUser] add constraint [FK_eduq.AppUser_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);
alter table eduq.[AppUser] add constraint [FK_eduq.AppUser_eduq.Gender_GenderId] foreign key ([GenderId]) references eduq.[Gender] ([Id]);
alter table eduq.[AppUser] add constraint [FK_eduq.AppUser_eduq.AppUser_CreatedById] foreign key ([CreatedById]) references eduq.[AppUser] ([Id]);
alter table eduq.[AppUser] add constraint [FK_eduq.AppUser_eduq.AppUser_ModifiedById] foreign key ([ModifiedById]) references eduq.[AppUser] ([Id]);

create table [eduq].[Role]
(
	Id int identity(1,1) primary key,
	[Name] varchar(75),
	[Description] varchar(300)
);

create table [eduq].[AppUserRole]
(
	Id uniqueidentifier primary key,
	AppUserId uniqueidentifier,
	RoleId int
);
alter table eduq.[AppUserRole] add constraint [FK_eduq.AppUserRole_eduq.AppUser_AppUserId] foreign key ([AppUserId]) references eduq.[AppUser] ([Id]);
alter table eduq.[AppUserRole] add constraint [FK_eduq.AppUserRole_eduq.Role_RoleId] foreign key ([RoleId]) references eduq.[Role] ([Id]);

create table [eduq].[FieldType]
(
	Id int identity(1,1) primary key,
	[Name] varchar(75),
	[Description] varchar(300),
	StateId int
);
alter table eduq.[FieldType] add constraint [FK_eduq.FieldType_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);

create table [eduq].[Field]
(
	Id uniqueidentifier primary key,
	[Name] varchar(75),
	DisplayName varchar(150),
	[Description] varchar(300),
	IsDefault bit,
	DefaultValue varchar(300),
	EntityId uniqueidentifier,
	FieldTypeId int,
	StateId int
);
alter table eduq.[Field] add constraint [FK_eduq.Field_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);
alter table eduq.[Field] add constraint [FK_eduq.Field_eduq.Entity_EntityId] foreign key ([EntityId]) references eduq.[Entity] ([Id]);
alter table eduq.[Field] add constraint [FK_eduq.Field_eduq.FieldType_FieldTypeId] foreign key ([FieldTypeId]) references eduq.[FieldType] ([Id]);

create table [eduq].[Form]
(
	Id uniqueidentifier primary key,
	[Name] varchar(75),
	DisplayName varchar(150),
	[Description] varchar(300),
	EntityId uniqueidentifier,
	IsDefault bit,
	StateId int
);
alter table eduq.[Form] add constraint [FK_eduq.Form_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);
alter table eduq.[Form] add constraint [FK_eduq.Form_eduq.Entity_EntityId] foreign key ([EntityId]) references eduq.[Entity] ([Id]);

create table [eduq].[Tab]
(
	Id uniqueidentifier primary key,
	[Name] varchar(75),
	DisplayName varchar(150),
	FormId uniqueidentifier
);
alter table eduq.[Tab] add constraint [FK_eduq.Tab_eduq.Form_FormId] foreign key ([FormId]) references eduq.[Form] ([Id]);

create table [eduq].[FormTab]
(
	Id uniqueidentifier primary key,
	FormId uniqueidentifier,
	TabId uniqueidentifier,
	TabRowNumber int,
	TabWidthSpace int
);
alter table eduq.[FormTab] add constraint [FK_eduq.FormTab_eduq.Form_FormId] foreign key ([FormId]) references eduq.[Form] ([Id]);
alter table eduq.[FormTab] add constraint [FK_eduq.FormTab_eduq.Tab_TabId] foreign key ([TabId]) references eduq.[Tab] ([Id]);

create table [eduq].[TabField]
(
	Id uniqueidentifier primary key,
	TabId uniqueidentifier,
	FieldId uniqueidentifier,
	IsFieldEditable bit,
	FieldRowNumber int,
	FieldWidthSpace int
);
alter table eduq.[TabField] add constraint [FK_eduq.TabField_eduq.Tab_TabId] foreign key ([TabId]) references eduq.[Tab] ([Id]);
alter table eduq.[TabField] add constraint [FK_eduq.TabField_eduq.Field_FieldId] foreign key ([FieldId]) references eduq.[Field] ([Id]);

create table [eduq].[View]
(
	Id uniqueidentifier primary key,
	[Name] varchar(75),
	DisplayName varchar(150),
	[Description] varchar(300),
	EntityId uniqueidentifier,
	IsDefault bit,
	StateId int
);
alter table eduq.[View] add constraint [FK_eduq.View_eduq.State_StateId] foreign key ([StateId]) references eduq.[State] ([Id]);
alter table eduq.[View] add constraint [FK_eduq.View_eduq.Entity_EntityId] foreign key ([EntityId]) references eduq.[Entity] ([Id]);

create table [eduq].[ViewField]
(
	Id uniqueidentifier primary key,
	ViewId uniqueidentifier,
	FieldId uniqueidentifier,
	FieldColumnNumber int
);
alter table eduq.[ViewField] add constraint [FK_eduq.ViewField_eduq.View_ViewId] foreign key ([ViewId]) references eduq.[View] ([Id]);
alter table eduq.[ViewField] add constraint [FK_eduq.ViewField_eduq.Field_FieldId] foreign key ([FieldId]) references eduq.[Field] ([Id]);
