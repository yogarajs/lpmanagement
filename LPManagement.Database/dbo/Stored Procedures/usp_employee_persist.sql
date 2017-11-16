
CREATE PROCEDURE [dbo].[usp_employee_persist]
	@employee_data [dbo].[lp_management_type] readonly
AS
	MERGE [dbo].[lp_management] targetTable  
	USING
	(
		SELECT 
			[launch_pad_code]   
			,[location]		    
			,[financial_year]
 			,[employee_id]       
			,[employee_name]    
			,[employee_e_mail]  
			,[business_layer]   
			,[practice]         
			,[lp_category]      
			,[training_location]
			,[work_location]    
			,[status]
			,[utilization]
			FROM @employee_data
	) sourceTable  
	ON 
	(
		[sourceTable].[employee_id] = [targetTable].[employee_id] 
	)
	WHEN MATCHED THEN    
	UPDATE SET  
		[targetTable].[business_layer] = [sourceTable].[business_layer]   
		,[targetTable].[practice] = [sourceTable].[practice]		  
		,[targetTable].[lp_category] = [sourceTable].[lp_category]
		,[targetTable].[training_location] = [sourceTable].[training_location]
		,[targetTable].[work_location] = [sourceTable].[work_location]
		,[targetTable].[status] = [sourceTable].[status]
		,[targetTable].[utilization] = [sourceTable].[utilization]
	WHEN NOT MATCHED THEN  
	INSERT
	(
		[launch_pad_code]   
		,[location]		    
		,[financial_year]
		,[employee_id]       
		,[employee_name]    
		,[employee_e_mail]  
		,[business_layer]   
		,[practice]         
		,[lp_category]      
		,[training_location]
		,[work_location] 
		,[status]
		,[utilization]
	)
	VALUES
	(
		[sourceTable].[launch_pad_code]   
		,[sourceTable].[location]		    
		,[sourceTable].[financial_year]
		,[sourceTable].[employee_id]       
		,[sourceTable].[employee_name]    
		,[sourceTable].[employee_e_mail]  
		,[sourceTable].[business_layer]   
		,[sourceTable].[practice]         
		,[sourceTable].[lp_category]      
		,[sourceTable].[training_location]
		,[sourceTable].[work_location]    
		,[sourceTable].[status]
		,[sourceTable].[utilization]
	);
