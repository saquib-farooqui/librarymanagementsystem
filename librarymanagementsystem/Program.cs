
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Numerics;

/*Library Management System
Features Include:

*/
public class HelloWorld
{
   public static string connection = "Data Source=KING\\SQLEXPRESS;Initial Catalog=consoleapplibrary;Integrated Security=True;Trust Server Certificate=True";
    public static void wel()
    {
        Console.WriteLine("*****************************************");
        Console.WriteLine("Welcome to the library management system ");
        Console.WriteLine("Options:");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.Write("Enter option: ");



    }
   public static void register()
    {
        Console.WriteLine("************************");
        Console.WriteLine("REGISTRATION FORM");
        Console.WriteLine();
        string user = checkusername();
        if (user == null)
        {
            Console.Clear();
            Console.WriteLine("Username already exists");
            register();
            
        }

        Console.Write("Enter a password: ");
        string password = Console.ReadLine();
        Console.Write("Enter a role: ");
        string role;
        role= Console.ReadLine();

        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "INSERT INTO login(username,password,role) VALUES(@username,@password,@role)";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@username",user);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@role", role);
        int rec = cmd.ExecuteNonQuery();

        if (rec > 0)
        {
            Console.WriteLine("Registration Successfull");
            login();
        }
        else
        {
            Console.WriteLine("Registration Failed");
        }
       
      }
    public static string checkusername()
    {
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        Console.Write("Enter a username: ");
        string user;
        user = Console.ReadLine();
        string query = "SELECT * FROM login WHERE username = @username";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@username", user);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            return null;
            
        }
        else
        {
            return user;
        }
       

    }
    public static void login()
    {
        Console.WriteLine("************************");
        Console.WriteLine("LOGIN FORM");
        Console.WriteLine();
        Console.Write("Enter a username: ");
        string user;
        user = Console.ReadLine();
        Console.Write("Enter a password: ");
        string password = Console.ReadLine();
        Console.Write("Enter a role: ");
        string role;
        role = Console.ReadLine();
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM login WHERE username = @username AND password = @password AND role = @role";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@username", user);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@role", role);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            Console.Clear();
            Console.WriteLine("Login Success - Welcome to the Application");
            menu();
          

           
           

        }
        else
        {
            Console.Clear();
            Console.WriteLine("Incorrect username or password - please retry");
         
            login();
        }
    }
    public static void menu()
    {
        Console.WriteLine("*********************************");
        Console.WriteLine("Library Management System");
        Console.WriteLine("Options:");
        Console.WriteLine("1. View Books");
        Console.WriteLine("2. Add Books");
        Console.WriteLine("3. Remove Books");
        Console.WriteLine("4. Loan Books");
        Console.WriteLine("5. Return Books");
        Console.WriteLine("6. Pay Fine");
        Console.WriteLine("7. Return History");
        Console.WriteLine("8. Exit Application");
        Console.Write("Enter an option: ");


        string option;
        option = Console.ReadLine();
        bool opval = int.TryParse(option, out int a);
        if (opval)
        {
            if (a == 1)
            {
                Console.Clear();
                viewbooks();
            }
            else if (a == 2)
            {
                Console.Clear();
                addbooks();
            }
            else if (a == 3)
            {

                Console.Clear();
                deletebooks();
            }
            else if (a == 4)
            {
                Console.Clear();
                rentbooks();
            }
            else if (a == 8)
            {
                Environment.Exit(0);
            }
            else if (a == 5)
            {
                Console.Clear();
                returnbook();
            }
            else if (a == 7)
            {
                Console.Clear();
                returnhistory();
            }
            else if (a == 6)
            {
                Console.Clear();
                payfine();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Option");
                menu();
            }
        }
        else
        {

            Console.Clear();
            Console.WriteLine("Invalid Entry - needs to be numerical");
            menu();

        }

    }
    public static void returnhistory()
    {
        Console.WriteLine("*****Return History******");
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM nofine";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("ID | Name | Author | Published Year | Rent Date | Expected Return | Actual Return |  Renter");
        Console.WriteLine("-----------------------------------------------------------------------------");
        while (reader.Read())
        {
            

            Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["author"]} | {reader["publishedyear"]} | {reader["rentdate"]} | {reader["expectedreturndate"]} | {reader["actualreturndate"]} |  {reader["renter"]}");


        }
        Console.Write("***Press 1 to return to the menu or 2 to exit the application: ");
        string op;
        op = Console.ReadLine();
        bool opval = int.TryParse(op, out int a);
        if (opval)
        {
            if (a == 1)
            {
                Console.Clear();
                menu();
            }
            else if (a == 2)
            {

                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid option");
               returnhistory();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("**Invalid Entry");
            returnhistory();

        }
    }
    public static void payfine()
    {
        Console.WriteLine("*****CURRENT FINE LIST******");
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM renturn";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("ID   | Name       | Author     | Published Year | Rent Date   | Expected Return | Actual Return | Fine | Renter");
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"],-3} | {reader["name"],-10} | {reader["author"],-10} | {reader["publishedyear"],-5} | {reader["rentdate"],-10} | {reader["expectedreturndate"],-15} | {reader["actualreturndate"],-10} | {reader["fine"],-4} | {reader["renter"],-10}");


        }
        Console.Write("Enter ID of fine to pay off: ");
        int id;
        id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter name of renter: ");
        string name   = Console.ReadLine();
        SqlConnection connect = new SqlConnection(connection);
        connect.Open();

        string querys = "SELECT * FROM renturn WHERE id=@id AND renter =@renter";
        SqlCommand cmds = new SqlCommand(querys, connect);
        cmds.Parameters.AddWithValue("@id", id);
        cmds.Parameters.AddWithValue("@renter", name);
        SqlDataReader readers = cmds.ExecuteReader();
        if (readers.Read())
        {
            int bookid = Convert.ToInt32(readers["id"]);
            string fname = readers["name"].ToString();
            string author = readers["author"].ToString();
            string fyear = readers["publishedyear"].ToString();
            DateTime rentdate = Convert.ToDateTime(readers["rentdate"]);
            DateTime returndate = Convert.ToDateTime(readers["expectedreturndate"]);
            DateTime actualdate = Convert.ToDateTime(readers["actualreturndate"]);
            string renter = readers["renter"].ToString();

            SqlConnection c = new SqlConnection(connection);
            c.Open();
            string q = "INSERT INTO nofine(id,name,author,publishedyear,rentdate,expectedreturndate,actualreturndate,renter) VALUES(@id,@name,@author,@publishedyear,@rentdate,@returndate,@actualreturndate,@renter)";
            SqlCommand sqlCommand = new SqlCommand(q, c);
            sqlCommand.Parameters.AddWithValue("@id", bookid);
            sqlCommand.Parameters.AddWithValue("@name", fname);
            sqlCommand.Parameters.AddWithValue("@author", author);
            sqlCommand.Parameters.AddWithValue("@publishedyear", fyear);
            sqlCommand.Parameters.AddWithValue("@rentdate", rentdate);
            sqlCommand.Parameters.AddWithValue("@returndate", returndate);
            sqlCommand.Parameters.AddWithValue("@actualreturndate", actualdate);
            sqlCommand.Parameters.AddWithValue("@renter", renter);

            SqlConnection a = new SqlConnection(connection);
            a.Open();

            string qu = "DELETE FROM renturn WHERE id=@id AND renter=@renter";
            SqlCommand ca = new SqlCommand(qu, c);
            ca.Parameters.AddWithValue("@id",id);
            ca.Parameters.AddWithValue("@renter", renter);

            int rec = sqlCommand.ExecuteNonQuery(); 
            int rec1 = ca.ExecuteNonQuery();

            if (rec > 0 && rec1 > 0)
            {
                Console.Clear();
                Console.WriteLine("Fine has been payed off and added to the history section");
                Console.Write("Would you like to pay more fines off? (1 = yes, 2 = menu, 3 = exit) ");
                int option;
                option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                {
                    Console.Clear();
                    payfine();

                }
                else if (option == 2)
                {
                    Console.Clear();
                    menu();

                }
                else if (option == 3)
                {

                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    menu();


                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Error Occured");
                payfine();
            }
          


        }
        else
        {
            Console.Clear();
            Console.WriteLine("Fine not found");
            payfine();
        }




    }






    public static void viewbooks()
    {
        Console.WriteLine("*************************");
        Console.WriteLine("CURRENT BOOK INVENTORY: ");

        SqlConnection con = new SqlConnection(connection);  
        con.Open();
        string query = "SELECT * FROM books";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("Name       | Author     | Published Year");
        Console.WriteLine("----------------------------------------");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["name"],-10} | {reader["author"],-10} | {reader["publishedyear"],-14}");


        }

        Console.Write("***Press 1 to return to the menu or 2 to exit the application: ");
        string op;
        op = Console.ReadLine();
        bool opval = int.TryParse(op, out int a);
        if (opval)
        {
            if (a == 1)
            {
                Console.Clear();
                menu();
            }
            else if (a == 2)
            {

                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid option");
                viewbooks();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("**Invalid Entry");
            viewbooks();
     
        }
    }

    public static void addbooks()
    {
        Console.WriteLine("***ADD BOOKS****");
       
        Console.Write("Book Name: ");
        string book;
        book = Console.ReadLine();
        Console.Write("Author: ");
        string author;
        author = Console.ReadLine();
        Console.Write("Publishing Year: ");
        string year;
        year = Console.ReadLine();

        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "INSERT INTO books(name,author,publishedyear) VALUES(@name,@author,@publisheyear)";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@name", book);
        cmd.Parameters.AddWithValue("@author", author);
        cmd.Parameters.AddWithValue("@publisheyear", year);
        int rec = cmd.ExecuteNonQuery();
        if (rec > 0)
        {
            Console.Clear();
            Console.WriteLine("Book Added");
            displaybooks();
            Console.Write("Would you like to add more books? (1 = yes, 2 = menu, 3 = exit) ");
            string o;
            o = Console.ReadLine();
            bool opvals = int.TryParse(o, out int a);
            if (opvals)
            {
                if (a == 1)
                {
                    Console.Clear();
                    addbooks();

                }
                else if (a == 2)
                {
                    Console.Clear();
                    menu();

                }
                else if (a == 3)
                {

                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Entry");
                    invalidaddbooks();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Entry");
                invalidaddbooks();

            }

        }

    }
    public static void invalidaddbooks()
    {
        Console.Write("Would you like to add more books? (1 = yes, 2 = menu, 3 = exit) ");
        string o;
        o = Console.ReadLine();
        bool opvals = int.TryParse(o, out int a);
        if (opvals)
        {
            if (a == 1)
            {
                Console.Clear();
                addbooks();

            }
            else if (a == 2)
            {
                Console.Clear();
                menu();

            }
            else if (a == 3)
            {

                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Entry");

                invalidaddbooks();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid Entry");
          
            invalidaddbooks();
        }
    }
    public static void deletebooks()
    {
        Console.WriteLine("*******DELETE BOOKS**********");
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM books";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("ID   | Name       | Author     | Published Year");
        Console.WriteLine("----------------------------------------------");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"],-4} | {reader["name"],-10} | {reader["author"],-10} | {reader["publishedyear"],-14}");

        }
       string id = checkifbookexists();
        if(id == null)
        {
            Console.Clear();
            Console.WriteLine("Book does not exist");
            deletebooks();
        }
        bool idval = int.TryParse(id, out int b);
        if (idval)
        {
            
                Console.Clear();
                SqlConnection connect = new SqlConnection(connection);
                connect.Open();
                string querys = "DELETE FROM books WHERE id=@id";
                SqlCommand cmds = new SqlCommand(querys, connect);
                cmds.Parameters.AddWithValue("@id", id);
                int rec = cmds.ExecuteNonQuery();
                if (rec > 0)
                {
                    Console.WriteLine("Book Deleted");
                    displaybooks();
                    Console.Write("Would you like to delete more books? (1 = yes, 2 = menu, 3 = exit) ");
                    string option;
                    option = Console.ReadLine();
                    bool opval = int.TryParse(option, out int a);
                    if (opval)
                    {
                        if (a == 1)
                        {
                            Console.Clear();
                            deletebooks();

                        }
                        else if (a == 2)
                        {
                            Console.Clear();
                            menu();

                        }
                        else if (a == 3)
                        {

                            Environment.Exit(0);
                        }
                         else
                            {
                        Console.Clear();
                        Console.WriteLine("Invalid number range");
                        invaliddeletebooks();
                    }   
                 
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Entry");
                        invaliddeletebooks();
                    }
                }
              


            
        }
        else
        {
            Console.Clear();
            Console.WriteLine("**INVALID ENTRY - MUST BE NUMERICAL");
            deletebooks();
        }

    }
    public static string checkifbookexists()
    {
        SqlConnection na = new SqlConnection(connection);
        na.Open();
        Console.Write("Enter ID of book you would like to delete: ");
        string id;
        id = Console.ReadLine();
        string query = "SELECT * FROM books WHERE id = @id";
        SqlCommand cmd= new SqlCommand(query, na);
        cmd.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            return id;
        }
        else
        {
            return null;
        }
    }
    public static void invaliddeletebooks()
    {
        Console.Write("Would you like to delete more books? (1 = yes, 2 = menu, 3 = exit) ");
        string o;
        o = Console.ReadLine();
        bool opvals = int.TryParse(o, out int a);
        if (opvals)
        {
            if (a == 1)
            {
                Console.Clear();
                deletebooks();

            }
            else if (a == 2)
            {
                Console.Clear();
                menu();

            }
            else if (a == 3)
            {

                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("invalid number range");
                invaliddeletebooks();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid Entry");

            invaliddeletebooks();
        }
    }

    public static string checkifbookrentable()
    {
        SqlConnection na = new SqlConnection(connection);
        na.Open();
        Console.Write("Enter the book ID to rent: ");
        string id;
        id = Console.ReadLine();
        string query = "SELECT * FROM books WHERE id = @id";
        SqlCommand cmd = new SqlCommand(query, na);
        cmd.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            return id;
        }
        else
        {
            return null;
        }
    }
    public static void rentbooks()
    {
        displaybooks();
        SqlConnection con = new SqlConnection(connection);
        con.Open();

        string id = checkifbookrentable();
        if(id == null)
        {
            Console.Clear();
            Console.WriteLine("Book does not exist");
            rentbooks();
        }
        string query = "SELECT * FROM books WHERE id =@id";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {

            int bookid = Convert.ToInt32(reader["id"]);
            string name = reader["name"].ToString();
            string author = reader["author"].ToString();
            string year = reader["publishedyear"].ToString();


            Console.Write("Initial Rent Date (yyyy-MM-dd): ");
            DateTime rentfrom;
            rentfrom = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Return Rent Date (yyyy-MM-dd): ");
            DateTime returna;
            returna = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Enter name of renter: ");
            string rentername;
            rentername = Console.ReadLine();
            

            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            string qu = "SELECT * FROM rents WHERE name = @name AND  ( (rentdate <= @newReturnDate AND returndate >= @newRentDate)  )";
            SqlCommand sqlCommand = new SqlCommand(qu, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@name", name);
            sqlCommand.Parameters.AddWithValue("@newReturnDate", returna);
            sqlCommand.Parameters.AddWithValue("@newRentDate", rentfrom);
            SqlDataReader read = sqlCommand.ExecuteReader();    

            if (read.HasRows)
            {
                Console.Clear();
                Console.WriteLine("Book is rented - please select a different book:");
                Console.WriteLine();
                Console.WriteLine();
                rentbooks();
            }
            else
            {
                SqlConnection cam = new SqlConnection(connection);
                cam.Open();
                string q = "INSERT INTO rents(id,name,author,publishedyear,rentdate,returndate,renter) VALUES(@id,@name,@author,@publishedyear,@rentdate,@returndate,@renter)";
                SqlCommand ca = new SqlCommand(q, cam);
                ca.Parameters.AddWithValue("@id", bookid);
                ca.Parameters.AddWithValue("@name", name);
                ca.Parameters.AddWithValue("@author", author);
                ca.Parameters.AddWithValue("@publishedyear", year);
                ca.Parameters.AddWithValue("@rentdate", rentfrom);
                ca.Parameters.AddWithValue("@returndate", returna);
                ca.Parameters.AddWithValue("@renter", rentername);
                int rec = ca.ExecuteNonQuery();
                if (rec > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Book rented");
                    Console.Write("Would you like to rent more books? (1 = yes, 2 = menu, 3 = exit) ");
                    string option;
                    option = Console.ReadLine();
                    bool opval = int.TryParse(option, out int a);
                    if (opval)
                    {
                        if (a ==1)
                        {
                            Console.Clear();
                            rentbooks();

                        }
                        else if (a==2)
                        {
                            Console.Clear();
                            menu();

                        }
                        else if (a==3)
                        {

                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Number Range");
                            invalidentryrentbook();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Entry");
                        invalidentryrentbook();
                    }


                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error");
                    rentbooks();
                }
            }
            
        }

  
    }
    public static void invalidentryrentbook()
    {
        Console.Write("Would you like to rent more books? (1 = yes, 2 = menu, 3 = exit) ");
        string o;
        o = Console.ReadLine();
        bool opvals = int.TryParse(o, out int a);
        if (opvals)
        {
            if (a == 1)
            {
                Console.Clear();
                rentbooks();

            }
            else if (a == 2)
            {
                Console.Clear();
                menu();

            }
            else if (a == 3)
            {

                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("invalid number range");
                invalidentryrentbook();
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Invalid Entry");

            invalidentryrentbook();
        }
    }
 public static void returnbook()
    {
        Console.WriteLine("*******LIST OF CURRENT RENTALS******");
        SqlConnection connec = new SqlConnection(connection);
        connec.Open();

        string querys= "SELECT * FROM rents";
        SqlCommand cmds = new SqlCommand(querys, connec);
        SqlDataReader rea = cmds.ExecuteReader();
        Console.WriteLine("ID   | Name       | Published Year | Rent Date   | Return Date | Renter     ");
        Console.WriteLine("--------------------------------------------------------------------------");
        while (rea.Read())
        {

            Console.WriteLine($"{rea["id"],-4} | {rea["name"],-10} | {rea["publishedyear"],-5} | {rea["rentdate"],-11} | {rea["returndate"],-12} | {rea["renter"],-10}");

        }

        string id = checkifrentalexists();
        if (id == null)
        {
            Console.Clear();
            Console.WriteLine("Rental does not exist");
            returnbook();
        }
        string rentername = checkifrenterexists();
        if (rentername == null)
        {
            Console.Clear();
            Console.WriteLine("Renter does not exist");
            returnbook();
        }
        Console.Write("Enter date of return (yyyy-mm-dd): ");
        DateTime rentalreturn;
        rentalreturn = Convert.ToDateTime(Console.ReadLine());

        SqlConnection connect = new SqlConnection(connection);
        connect.Open();
        string query = "SELECT * FROM rents WHERE id = @id AND renter = @renter";
        SqlCommand cmd = new SqlCommand(query, connect);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@renter", rentername);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine("Book found");
            int bookid = Convert.ToInt32(reader["id"]);
            string bookname = reader["name"].ToString();
            string author = reader["author"].ToString();
            string year = reader["publishedyear"].ToString();
            DateTime rentdate = Convert.ToDateTime(reader["rentdate"]);
            DateTime returndate = Convert.ToDateTime(reader["returndate"]);
            string renter = reader["renter"].ToString();

            SqlConnection sqlConnection = new SqlConnection(connection);    
            sqlConnection.Open();
            string q = "DELETE FROM rents WHERE id =@id AND renter =@name";
            SqlCommand cmd2 = new SqlCommand(q, sqlConnection);
            cmd2.Parameters.AddWithValue("@id", id);
            cmd2.Parameters.AddWithValue("@name" ,rentername);
            double fineperday = .50;
            double totalfine = 0;
            if (rentalreturn > returndate)
            {
                int dayslate = (rentalreturn - returndate).Days;

                 totalfine = dayslate * fineperday;
                SqlConnection newcon = new SqlConnection(connection);
                newcon.Open();
                string a = "INSERT INTO renturn(id,name,author,publishedyear,rentdate,expectedreturndate,actualreturndate,fine,renter) VALUES(@id,@name,@author,@year,@rentdate,@expectedreturndate,@actualreturndate,@fine,@renter)";
                SqlCommand ca = new SqlCommand(a, newcon);
                ca.Parameters.AddWithValue("@id", bookid);
                ca.Parameters.AddWithValue("@name", bookname);
                ca.Parameters.AddWithValue("@author", author);
                ca.Parameters.AddWithValue("@year", year);
                ca.Parameters.AddWithValue("@rentdate", rentdate);
                ca.Parameters.AddWithValue("@expectedreturndate", returndate);
                ca.Parameters.AddWithValue("@actualreturndate", rentalreturn);
                ca.Parameters.AddWithValue("@fine", totalfine);
                ca.Parameters.AddWithValue("@renter", renter);

                int rec = cmd2.ExecuteNonQuery();
                int rec1 = ca.ExecuteNonQuery();
                if(rec > 0 && rec1 > 0)
                {
                    Console.WriteLine("Book Returned");
                    Console.Write("Would you like to return more books? (1 = yes, 2 = menu, 3 = exit) ");
                    int option;
                    option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.Clear();
                        returnbook();

                    }
                    else if (option == 2)
                    {
                        Console.Clear();
                        menu();

                    }
                    else if (option == 3)
                    {

                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        menu();


                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error couldnt return book - try again");
                    returnbook();
                }
            }
            else
            {
                
                SqlConnection newcon = new SqlConnection(connection);
                newcon.Open();
                string a = "INSERT INTO nofine(id,name,author,publishedyear,rentdate,expectedreturndate,actualreturndate,renter) VALUES(@id,@name,@author,@year,@rentdate,@expectedreturndate,@actualreturndate,@renter)";
                SqlCommand ca = new SqlCommand(a, newcon);
                ca.Parameters.AddWithValue("@id", bookid);
                ca.Parameters.AddWithValue("@name", bookname);
                ca.Parameters.AddWithValue("@author", author);
                ca.Parameters.AddWithValue("@year", year);
                ca.Parameters.AddWithValue("@rentdate", rentdate);
                ca.Parameters.AddWithValue("@expectedreturndate", returndate);
                ca.Parameters.AddWithValue("@actualreturndate", rentalreturn);
                ca.Parameters.AddWithValue("@renter", renter);
             

                int rec = cmd2.ExecuteNonQuery();
                int rec1 = ca.ExecuteNonQuery();
                if (rec > 0 && rec1 > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Book Returned");

                    Console.Write("Would you like to return more books? (1 = yes, 2 = menu, 3 = exit) ");
                    int option;
                    option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        Console.Clear();
                        returnbook();

                    }
                    else if (option == 2)
                    {
                        Console.Clear();
                        menu();

                    }
                    else if (option == 3)
                    {

                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        menu();


                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Error couldnt return book - try again");
                    returnbook();
                }
            }
       


        }
        else
        {
            Console.Clear();
            Console.WriteLine("Cannot find renter or incorrect id - please try again");
            returnbook();
        }

    }
    public static string checkifrentalexists()
    {
        Console.Write("Enter id of book returning: ");

        string id;
        id = Console.ReadLine();
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM rents WHERE id =@id";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@id", id);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            return id;

        }
        else
        {
            return null;
        }
    }

    public static string checkifrenterexists()
    {
        Console.Write("Enter name of renter: ");
        string rentername;
        rentername = Console.ReadLine();
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM rents WHERE renter =@renter";
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.Parameters.AddWithValue("@renter", rentername);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            return rentername;

        }
        else
        {
            return null;
        }
    }
    public static void displaybooks()
    {
        Console.WriteLine("******LIST OF CURRENT BOOKS*********");
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        string query = "SELECT * FROM books";
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        Console.WriteLine("ID   | Name       | Author     | Published Year");
        Console.WriteLine("----------------------------------------------");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["id"],-4} | {reader["name"],-10} | {reader["author"],-10} | {reader["publishedyear"],-14}");


        }
    }
    public static void Main(string[] args)
    {


        wel();
        string option;
        option = Console.ReadLine();

        bool opval = int.TryParse(option, out int a);
        while (true)
        {
            if (opval)
            {

                if (a == 2)
                {
                    Console.Clear();
                    register();
                }
                else if (a == 1)
                {
                    Console.Clear();
                    login();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection");
                wel();
                option = Console.ReadLine();
                opval = int.TryParse(option, out a);
            }
        } 
    }
}
