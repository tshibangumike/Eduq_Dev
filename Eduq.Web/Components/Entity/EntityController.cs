using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Eduq.Model;
using Eduq.Service;
using Eduq.Web.Components.Base;

namespace Eduq.Web.Components.Entity
{
    public class EntityController : BaseController
    {

        public JsonResult GetEntities()
        {
            try
            {
                var records = EntityService.QueryEntities();
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetEntityById(Guid entityId)
        {
            try
            {
                var record = EntityService.QueryEntityById(entityId);
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateEntity(List<KeyValue> keyValues)
        {
            try
            {

                var id = Guid.NewGuid();
                var name = keyValues.FirstOrDefault(x => x.Key == "Name")?.Value;
                var displayName = keyValues.FirstOrDefault(x => x.Key == "DisplayName")?.Value;
                var pluralName = keyValues.FirstOrDefault(x => x.Key == "PluralName")?.Value;

                var fieldTypes = FieldTypeService.QueryFieldTypes();

                #region Create entity record
                var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Entity_create]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@id", id),
                            new SqlParameter("@name", (name.ToLower())),
                            new SqlParameter("@displayName", displayName),
                            new SqlParameter("@pluralName", pluralName),
                            new SqlParameter("@stateId", 1)
                            });

                if (returnValue != 1)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                var newlyCreatedEntity = EntityService.QueryEntityById(id);
                if (newlyCreatedEntity == null)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Create sql table
                var returnValueCreateSqlTable = SqlService.CreateSqlTable(name);

                if (!returnValueCreateSqlTable)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                /*Create Fields*/
                #region Id Field
                /*Id*/
                var fieldId = Guid.NewGuid();
                var fieldName = "Id";
                var fieldDisplayName = "Id";
                var fieldDescription = "";
                var fieldDefaultValue = "";
                var fieldEntityId = id;
                var GuidFieldType = FieldTypeService.QueryFieldTypeByName("Guid");
                var GuidFieldTypeId = (int)GuidFieldType.FirstOrDefault(x => x.Key == "Id").Value;
                var GuidFieldTypeSqlDataType = GuidFieldType.FirstOrDefault(x => x.Key == "SqlDataTypeName").Value.ToString();
                var fieldStateId = 1;
                var returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, GuidFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Name Field
                /*Name*/
                fieldId = Guid.NewGuid();
                fieldName = "Name";
                fieldDisplayName = "Name";
                fieldDescription = "";
                fieldDefaultValue = "";
                fieldEntityId = id;
                var texteFieldType = FieldTypeService.QueryFieldTypeByName("Texte");
                var texteFieldTypeId = (int)texteFieldType.FirstOrDefault(x => x.Key == "Id").Value;
                fieldStateId = 1;
                returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, texteFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region CreatedOn Field
                /*CreatedOn*/
                fieldId = Guid.NewGuid();
                fieldName = "CreatedOn";
                fieldDisplayName = "Date de Création";
                fieldDescription = "";
                fieldDefaultValue = "";
                fieldEntityId = id;
                var dateTimeFieldType = FieldTypeService.QueryFieldTypeByName("Date et Heure");
                var dateTimeFieldTypeId = (int)dateTimeFieldType.FirstOrDefault(x => x.Key == "Id").Value;
                fieldStateId = 1;
                returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, dateTimeFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region CreatedById Field
                /*CreatedId*/
                fieldId = Guid.NewGuid();
                fieldName = "CreatedById";
                fieldDisplayName = "Créer par";
                fieldDescription = "";
                fieldDefaultValue = "";
                fieldEntityId = id;
                var lookupFieldType = FieldTypeService.QueryFieldTypeByName("Recherche");
                var lookupFieldTypeId = (int)lookupFieldType.FirstOrDefault(x => x.Key == "Id").Value;
                fieldStateId = 1;
                returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, lookupFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region ModifiedOn
                /*ModifiedOn*/
                fieldId = Guid.NewGuid();
                fieldName = "ModifiedOn";
                fieldDisplayName = "Date de modification";
                fieldDescription = "";
                fieldDefaultValue = "";
                fieldEntityId = id;
                fieldStateId = 1;
                returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, dateTimeFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region ModifiedById Field
                /*ModifiedById*/
                fieldId = Guid.NewGuid();
                fieldName = "ModifiedById";
                fieldDisplayName = "Modifié par";
                fieldDescription = "";
                fieldDefaultValue = "";
                fieldEntityId = id;
                lookupFieldType = FieldTypeService.QueryFieldTypeByName("Recherche");
                lookupFieldTypeId = (int)lookupFieldType.FirstOrDefault(x => x.Key == "Id").Value;
                fieldStateId = 1;
                returnValueCreateField = FieldService.CreateField(fieldId, fieldName, fieldDisplayName, fieldDescription, fieldDefaultValue, fieldEntityId, lookupFieldTypeId, true, fieldStateId);
                if (returnValueCreateField == Guid.Empty)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                return Json(new { message = id, type = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateEntity(List<KeyValue> keyValues)
        {
            try
            {

                var id = keyValues.FirstOrDefault(x => x.Key == "Id")?.Value;
                var name = keyValues.FirstOrDefault(x => x.Key == "Name")?.Value;
                var displayName = keyValues.FirstOrDefault(x => x.Key == "DisplayName")?.Value;
                var pluralName = keyValues.FirstOrDefault(x => x.Key == "PluralName")?.Value;
                var isDisplayOnSideMenu = keyValues.FirstOrDefault(x => x.Key == "IsDisplayOnSideMenu")?.Value;
                var sideMenuParentItemId = keyValues.FirstOrDefault(x => x.Key == "SideMenuParentItemId")?.Value;
                var sideMenuItemPosition = keyValues.FirstOrDefault(x => x.Key == "SideMenuItemPosition")?.Value;

                var fieldTypes = FieldTypeService.QueryFieldTypes();

                #region Update entity record
                var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Entity_update]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@id", id),
                            new SqlParameter("@name", name),
                            new SqlParameter("@displayName", displayName),
                            new SqlParameter("@pluralName", pluralName),
                            new SqlParameter("@isDisplayOnSideMenu", isDisplayOnSideMenu),
                            new SqlParameter("@sideMenuParentItemId", sideMenuParentItemId),
                            new SqlParameter("@sideMenuItemPosition", sideMenuItemPosition),
                            new SqlParameter("@stateId", 1)
                            });

                if (returnValue != 1)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                var newlyCreatedEntity = EntityService.QueryEntityById(Guid.Parse(id));
                if (newlyCreatedEntity == null)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'entité!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                return Json(new { message = id, type = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteEntity(Guid entityId)
        {

            try
            {

                var entity = EntityService.QueryEntityById(entityId);
                if(entity == null)
                {
                    return Json(new { message = "Le système n'a pas trouvé l'enregistrement à supprimer!", type = "error" }, JsonRequestBehavior.AllowGet);
                }

                #region Delete Field Records
                var fieldsDeleteError = false;
                var fields = FieldService.QueryFieldsByEntityId(entityId);
                foreach(var field in fields)
                {
                    var fieldId = (Guid)field.FirstOrDefault(x => x.Key == "Id").Value;
                    var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Field_deletefieldbyfieldid]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@fieldId", fieldId)
                            });
                    if (returnValue != 1)
                    {
                        return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                    }
                    var deletedField = FieldService.QueryFieldById(fieldId);
                    if (deletedField != null)
                    {
                        fieldsDeleteError = true;
                    }
                }
                if (fieldsDeleteError)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Delete Entity Record
                var returnValueDeleteEntity = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Entity_deleteentitybyentityid]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@entityId", entityId)
                            });
                if(returnValueDeleteEntity != 1)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                var deletedEntity = EntityService.QueryEntityById(entityId);
                if (deletedEntity != null)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Delete Sql Table
                var entityName = entity.FirstOrDefault(x => x.Key == "Name").Value.ToString();
                var returnValueDeleteSqlTableColumn = SqlService.DeleteSqlTable(entityName);
                if (!returnValueDeleteSqlTableColumn)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                return Json(new { message = "L'entité a été supprimé avec succès!.", type = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}