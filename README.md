# DatabaseLibrary
 Use database easier

# How to use
 ## Connect
  1. Set DatabaseManager instance with Controller class.<br/>
   ex) var test = DatabaseManager(new SqlServerController());
  
  2. Use Connect function. Return true when it is connected successfully.<br/>
   ex) var check = test.Connect(DB_Address, DB_Identifier, User ID, Password);
   
 ## Send query
  1. After connection, Use SendQuery function. You can get a result as -1 or number of row affected by the query.<br/>
   ex) var result = test.SendQuery(Query);
   
 ## Get data
  1. Get data set: Return many tables at once.<br/>
   ex) var data = test.GetDataSet(Query);
   
  2. Get data table: Return one table.<br/>
   ex) var data - test.GetDataTable(Query);
