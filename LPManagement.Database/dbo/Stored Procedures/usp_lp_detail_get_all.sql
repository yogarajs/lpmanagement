CREATE PROCEDURE [dbo].[usp_lp_detail_get_all]
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
