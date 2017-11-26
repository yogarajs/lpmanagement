DROP PROCEDURE [dbo].[usp_lp_detail_get_all]
GO

CREATE PROCEDURE [dbo].[usp_lp_detail_get_all]
	@record_count int output
AS
	SELECT
		[lpd].[launch_pad_code]  
		,[lpd].[location]         
		,[lpd].[financial_year]   
		,[lpd].[business_layer]   
		,[lpd].[practice]         
		,[lpd].[lp_category]      
		,[lpd].[status]            
	FROM
		[lp_detail] [lpd]

	SET @record_count = @@ROWCOUNT