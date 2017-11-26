CREATE PROCEDURE [dbo].[usp_lp_detail_get_all_by_launch_pad_code]
	@launch_pad_code nvarchar(50),
	@record_count int output
AS
	SELECT
		[lpd].[launch_pad_code]  
		,[lpd].[status]            
		,[lps].[overall_score]
	FROM
		[lp_detail] [lpd]
	INNER JOIN
		[lp_score] [lps] ON [lps].[employee_id] = [lpd].[employee_id]
	WHERE 
		[lpd].[launch_pad_code] = @launch_pad_code

	SET @record_count = @@ROWCOUNT