﻿DROP TABLE [dbo].[lp_score] 
GO

CREATE TABLE [dbo].[lp_score] (
    [employee_score_id]            INT            IDENTITY (1, 1) NOT NULL,
    [employee_id]                  INT            NOT NULL,
    [faculty]                      NVARCHAR (250) NULL,
    [unix]                         FLOAT (53)     NULL,
    [software_engineering_and_uml] FLOAT (53)     NULL,
    [oracle_SQL_and_PL_SQL]        FLOAT (53)     NULL,
    [dot_net]                      FLOAT (53)     NULL,
    [core_java]                    FLOAT (53)     NULL,
    [advance_java]                 FLOAT (53)     NULL,
    [J2EE]                         FLOAT (53)     NULL,
    [hibernate]                    FLOAT (53)     NULL,
    [spring]                       FLOAT (53)     NULL,
    [html]                         FLOAT (53)     NULL,
    [java_script]                  FLOAT (53)     NULL,
    [informatica]                  FLOAT (53)     NULL,
    [talent_development]           FLOAT (53)     NULL,
    [manual_testing]               FLOAT (53)     NULL,
    [UFT]                          FLOAT (53)     NULL,
    [selenium]                     FLOAT (53)     NULL,
    [OOPS]                         FLOAT (53)     NULL,
    [RDBMS]                        FLOAT (53)     NULL,
    [final_exam]                   FLOAT (53)     NULL,
    [overall_score]                FLOAT (53)     NULL,
    [project_score]                FLOAT (53)     NULL,
    [project_reviewer]             NVARCHAR (250) NULL,
    [overall_feedback]             FLOAT (53)     NULL,
    [faculty_feedback]             FLOAT (53)     NULL,
    [remarks]                      NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([employee_score_id] ASC)
);

