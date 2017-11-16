CREATE TYPE [dbo].[lp_management_type] AS TABLE (
    [launch_pad_code]   NVARCHAR (50)  NOT NULL,
    [location]          NVARCHAR (50)  NOT NULL,
    [financial_year]    INT            NOT NULL,
    [employee_id]       INT            NOT NULL,
    [employee_name]     NVARCHAR (250) NOT NULL,
    [employee_e_mail]   NVARCHAR (250) NOT NULL,
    [business_layer]    NCHAR (10)     NOT NULL,
    [practice]          NCHAR (50)     NOT NULL,
    [lp_category]       NVARCHAR (50)  NOT NULL,
    [training_location] NVARCHAR (50)  NOT NULL,
    [work_location]     NVARCHAR (50)  NULL,
    [status]            NVARCHAR (50)  NULL,
    [utilization]       INT            NULL);

