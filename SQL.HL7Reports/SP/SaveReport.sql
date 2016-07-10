CREATE PROCEDURE [dbo].[SaveReport]
	@XML XML
AS
BEGIN
	INSERT INTO [dbo].[Report]
        ([ReportDateTime]
        ,[ImportedOn]
        ,[ReportText]
		,	PatientID 
		,	PatientName 
		,	AccessionNumber 
		,	[ReportXML] 
        ,[ActiveTF]        ,[CreatedOn]        ,[UID])
SELECT
		xc.value('(ReportDateTime)[1]', 'DateTime2') AS ReportdateTime,
		xc.value('(ImportedOn)[1]', 'DateTime2') AS ImportedOn,
		xc.value('(ReportText)[1]', 'VARCHAR(MAX)')  AS ReportText,
		xc.value('(PatientID)[1]', 'VARCHAR(1000)')  AS PatientID,
		xc.value('(Patientname)[1]', 'VARCHAR(2000)')  AS PatientName,
		xc.value('(AccessionNumber)[1]', 'VARCHAR(100)')  AS AccessionNumber,
		@XML,
		1,getDate() CreatedOn ,NEWID() [UID]
FROM 
	@XML.nodes('/Report') AS xt(xc)

DECLARE @reportID BIGINT
SELECT @reportID = @@IDENTITY
  
INSERT INTO [dbo].[Field]
           ([Sequence]
           ,[FieldNameID]
           ,[ReportID]
           ,[Value]
           ,[ActiveTF]           ,[CreatedOn]           ,[UID])  
SELECT
			xc.value('(Sequence)[1]', 'INT')  AS Seq,
			xc.value('(FieldName/FieldNameID)[1]', 'BIGINT') AS FieldnameID,
			@reportID ReportID,
			xc.value('(Value)[1]', 'VARCHAR(MAX)') AS Value,
			1,   getDate() CreatedOn, newID() [UID]
   FROM 
  @XML.nodes('/Report/ReportFields/Field') AS xt(xc)
  END