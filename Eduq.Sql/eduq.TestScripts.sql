USE [Eduq_VF_Dev] 

go 

/****** Object:  StoredProcedure [eduq].[Sidemenuitem_queryall]    Script Date: 2018/03/11 6:26:25 PM ******/ 
SET ansi_nulls ON 

go 

SET quoted_identifier ON 

go 

CREATE PROCEDURE [eduq].[Appuser_queryall] 
AS 
  BEGIN 
      SET nocount ON; 

      SELECT appuser.id            'Id', 
             appuser.[name]        'Name', 
             appuser.genderid      'GenderId', 
             gender.[name]         'GenderName', 
             appuser.emailaddress  'EmailAddress', 
             appuser.contactnumber 'ContactNumber' 
      FROM   eduq.appuser AppUser 
             LEFT JOIN eduq.gender Gender 
                    ON gender.id = appuser.genderid 
  END 