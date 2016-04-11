cls
@ECHO --------------------------Structure----------------- >  ENET_Project_Active_Learning_Group4_CleanDB.sql
type 1_Setup\Struct.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Index--------------------- >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 2_Index\Index.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Function------------------ >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 3_Function\Function.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------View---------------------- >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 4_View\View.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------SP------------------------ >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 5_SP\SP.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Data---------------------- >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 6_Data\Data.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
@ECHO --------------------------Others---------------------- >> ENET_Project_Active_Learning_Group4_CleanDB.sql
type 7_Others\Patch_DBVersion.sql >> ENET_Project_Active_Learning_Group4_CleanDB.sql
pause>nul
