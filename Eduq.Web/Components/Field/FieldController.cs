using Eduq.Model;
using Eduq.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduq.Web.Components.Field
{
    public class FieldController : Controller
    {

        public JsonResult GetFields()
        {
            try
            {
                var records = FieldService.QueryFields();
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFieldById(Guid fieldId)
        {
            try
            {
                var record = FieldService.QueryFieldById(fieldId);
                return Json(record, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFieldsByEntityId(Guid entityId)
        {
            try
            {
                var records = FieldService.QueryFieldsByEntityId(entityId);
                return Json(records, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateField(List<KeyValue> keyValues)
        {
            try
            {

                var id = Guid.NewGuid();
                var name = keyValues.FirstOrDefault(x => x.Key == "Name")?.Value;
                var displayName = keyValues.FirstOrDefault(x => x.Key == "DisplayName")?.Value;
                var description = keyValues.FirstOrDefault(x => x.Key == "Description")?.Value;
                var entityId = keyValues.FirstOrDefault(x => x.Key == "EntityId")?.Value;
                var entityName = keyValues.FirstOrDefault(x => x.Key == "EntityName")?.Value;
                var fieldTypeId = keyValues.FirstOrDefault(x => x.Key == "FieldTypeId")?.Value;
                var fieldTypeName = keyValues.FirstOrDefault(x => x.Key == "FieldTypeName")?.Value;
                var isRequired = keyValues.FirstOrDefault(x => x.Key == "IsRequired")?.Value;
                var lookupEntityName = keyValues.FirstOrDefault(x => x.Key == "LookupEntityName")?.Value;

                #region Create field record
                var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Field_create]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@id", id),
                            new SqlParameter("@name", name),
                            new SqlParameter("@displayName", displayName),
                            new SqlParameter("@description", description),
                            new SqlParameter("@entityId", entityId),
                            new SqlParameter("@fieldTypeId", fieldTypeId),
                            new SqlParameter("@isRequired", isRequired),
                            new SqlParameter("@stateId", 1)
                            });

                if (returnValue != 1)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                var newlyCreatedField = FieldService.QueryFieldById(id);
                if (newlyCreatedField == null)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Create sql table column
                var returnValueCreateSqlTableColumn = SqlService.CreateSqlTableColumn(entityName, name, fieldTypeName, (isRequired == "true" ? "NOT NULL": ""));
                if(!returnValueCreateSqlTableColumn)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Create sql foreign key constraint
                if(lookupEntityName == null)
                {
                    return Json(new { message = id, type = "success" }, JsonRequestBehavior.AllowGet);
                }
                var returnValueCreateSqlForeignKeyConstraint = SqlService.CreateSqlForeignKeyConstraint(entityName, lookupEntityName, name);
                if (!returnValueCreateSqlForeignKeyConstraint)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la creation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                return Json(new { message = id, type = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message, type = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteField(Guid fieldId)
        {
            try
            {
                var field = FieldService.QueryFieldById(fieldId);
                if(field == null)
                {
                    return Json(new { message = "Le système n'a pas trouvé l'enregistrement à supprimer!", type = "error" }, JsonRequestBehavior.AllowGet);
                }

                #region Delete Field Record
                var returnValue = SharedService.ExecutePostSqlStoredProcedure("[eduq].[Field_deletefieldbyfieldid]",
                            new List<SqlParameter>
                            {
                            new SqlParameter("@fieldId", fieldId)
                            });
                var deletedField = FieldService.QueryFieldById(fieldId);
                if (deletedField != null)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                #region Delete Sql Table Column
                var name = field.FirstOrDefault(x => x.Key == "Name").Value.ToString();
                var entityName = field.FirstOrDefault(x => x.Key == "EntityName").Value.ToString();

                var returnValueDeleteSqlTableColumn = SqlService.DeleteSqlTableColumn(entityName, name);

                if (!returnValueDeleteSqlTableColumn)
                {
                    return Json(new { message = "Une erreur s'est produise pendant la supprimation de l'attribut!", type = "error" }, JsonRequestBehavior.AllowGet);
                }
                #endregion

                return Json(new { message = "L'attribut a été supprimé avec succès!.", type = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}