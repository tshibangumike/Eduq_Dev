
SET ANSI_NULLS ON GO
SET QUOTED_IDENTIFIER ON GO -- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE eduq.Entity_QueryAllEntities -- Add the parameters for the stored procedure here
AS BEGIN -- SET NOCOUNT ON added to prevent extra result sets from
 -- interfering with SELECT statements.

SET NOCOUNT ON; -- Insert statements for procedure here

SELECT [Entity].Id,
       [Entity].Name,
       [Entity].DisplayName
FROM [eduq].[Entity] [Entity] END GO