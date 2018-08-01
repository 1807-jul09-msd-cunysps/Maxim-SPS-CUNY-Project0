using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using ContactLibrary;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;





namespace ContactClient
{
    
    class Program
    {
        
        
        static void Main(string[] args)
        {
        
        Boolean Notconnected = true;
        SqlConnection myConnection = new SqlConnection(
            "user id=mtill;" +
            "password=Deutchland11;server=rev-cuny-maxt.database.windows.net;" + 
            "Trusted_Connection=no;" + 
            "database=ContactApp; " + 
            "connection timeout=5");
            try
            {
                myConnection.Open();
                Notconnected = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("\n\n\n\n\n");
                Notconnected = true;
            }
            myConnection.Close();

            SqlCommand displayall= new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                   "areaCode,number,ext,houseNum,street,city,[state],country,zipcode FROM Person, " +
                   "Phone, [Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid; "
                   , myConnection);

            String by = "";
            SqlCommand searchByFirst = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode,"+
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone,"+
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( FirstName , @by ) > 3; "
                , myConnection);
            SqlCommand searchByLast = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( LastName , @by ) > 3; "
                , myConnection);
            SqlCommand searchByAddress = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( street , @by ) > 3; "
                , myConnection);
            SqlCommand searchByCity = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( city , @by ) > 3; "
                , myConnection);
            SqlCommand searchByCountry = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( country , @by ) > 3; "
                , myConnection);
            SqlCommand searchByAreaCode = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( areaCode , @by ) > 3; "
                , myConnection);
            SqlCommand searchByState = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( state , @by ) > 3; "
                , myConnection);
            SqlCommand searchByPhone = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( number , @by ) > 3; "
                , myConnection);
            SqlCommand searchByZip = new SqlCommand("SELECT Person.PiD,FirstName,LastName,countryCode," +
                "areaCode, number, ext, houseNum, street, city,[state], country, zipcode FROM Person, Phone," +
                $"[Address] WHERE Person.PiD = Phone.Pid AND Person.PiD = [Address].Pid AND DIFFERENCE( zipcode , @by ) > 2; "
                , myConnection);

            SqlCommand updateStuff = new SqlCommand(" UPDATE [Address] SET houseNum = @hnum, street = @street, state = @state, country = @country, " +
                "zipcode = @zip, city = @city WHERE Pid = @pid; UPDATE Phone SET countryCode = countrycode, areaCode = @area, number = @number, ext = @ext WHERE Pid = @pid; "
                , myConnection);

            updateStuff.Parameters.Add("@pid", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@hnum", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@street", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@city", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@state", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@country", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@zip", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@countrycode", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@area", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@number", SqlDbType.VarChar);
            updateStuff.Parameters.Add("@ext", SqlDbType.VarChar);

            searchByFirst.Parameters.Add("@by", SqlDbType.Text);
            searchByLast.Parameters.Add("@by", SqlDbType.Text);
            searchByAddress.Parameters.Add("@by", SqlDbType.Text);
            searchByCity.Parameters.Add("@by", SqlDbType.Text);
            searchByState.Parameters.Add("@by", SqlDbType.Text);
            searchByZip.Parameters.Add("@by", SqlDbType.Text);
            searchByCountry.Parameters.Add("@by", SqlDbType.Text);
            searchByAreaCode.Parameters.Add("@by", SqlDbType.Text);
            searchByPhone.Parameters.Add("@by", SqlDbType.Text);

            SqlCommand insertstuff= new SqlCommand("INSERT INTO Person ([Pid],[FirstName],[LastName])" +
                "VALUES (@pid,@fName,@lname);" +
                "INSERT INTO Address ([Pid],[houseNum],[street],[city],[state],[country],[zipcode])"+
                " VALUES (@pid, @hnum, @street, @city, @state, @country, @zip);" +
                "INSERT INTO Phone ([Pid], [countryCode], [areaCode], [number], [ext])"+
                "VALUES (@pid,@countrycode,@area,@number,@ext);"
                , myConnection);
            insertstuff.Parameters.Add("@pid", SqlDbType.Text);
            insertstuff.Parameters.Add("@fName", SqlDbType.Text);
            insertstuff.Parameters.Add("@lname", SqlDbType.Text);
            insertstuff.Parameters.Add("@hnum", SqlDbType.Text);
            insertstuff.Parameters.Add("@street", SqlDbType.Text);
            insertstuff.Parameters.Add("@city", SqlDbType.Text);
            insertstuff.Parameters.Add("@state", SqlDbType.Text);
            insertstuff.Parameters.Add("@country", SqlDbType.Text);
            insertstuff.Parameters.Add("@zip", SqlDbType.Text);
            insertstuff.Parameters.Add("@countrycode", SqlDbType.Text);
            insertstuff.Parameters.Add("@area", SqlDbType.Text);
            insertstuff.Parameters.Add("@number", SqlDbType.Text);
            insertstuff.Parameters.Add("@ext", SqlDbType.Text);

            SqlCommand dropwhere = new SqlCommand("DELETE FROM Person WHERE PiD  = @pid;" +
                                                  "DELETE FROM Phone WHERE Pid   = @pid; "+
                                                  "DELETE FROM Address WHERE Pid = @pid;"
                                                    , myConnection);
            dropwhere.Parameters.Add("@pid", SqlDbType.VarChar);

            void sqlSelect(SqlCommand X) {
                try
                {
                    
                    SqlDataReader myReader = null;

                    myConnection.Open();
                    Console.Write("Results:\n");
                    myReader = X.ExecuteReader();
                    
                    while (myReader.Read())
                    {

                        Console.Write(myReader["Pid"].ToString() + " ");
                        Console.Write(myReader["FirstName"].ToString() + " ");
                        Console.Write(myReader["LastName"].ToString() + "|");
                        Console.Write(myReader["countryCode"].ToString() + " ");
                        Console.Write(myReader["areaCode"].ToString() + " ");
                        Console.Write(myReader["number"].ToString() + " ");
                        Console.Write(myReader["ext"].ToString() + "|");
                        Console.Write(myReader["houseNum"].ToString() + " ");
                        Console.Write(myReader["street"].ToString() + " ");
                        Console.Write(myReader["city"].ToString() + " ");
                        Console.Write(myReader["state"].ToString() + " ");
                        Console.Write(myReader["country"].ToString() + " ");
                        Console.Write(myReader["zipcode"].ToString() + "\n");
                        Console.WriteLine("------------------------------------------------------------------------------------------");
                    }

                    myConnection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
       
        Console.WriteLine("Welcome to the Phone Contact App.");
        Console.WriteLine("Please select one of the following options:");
        Console.WriteLine("1: Display all contacts.");
        Console.WriteLine("2: Search contacts.");
        Console.WriteLine("3: Add or Update Contact.");
        Console.WriteLine("4: Remove a Contact.");
        Console.WriteLine("5: display menu");
        Console.WriteLine("6: clear screen");
        Console.WriteLine("7: Backup DatabaseData");
        Console.WriteLine("8: View backup");
        Console.WriteLine("9: Serialize Backup");
        Console.WriteLine("0: Close The Application");


            if (Notconnected)
                Console.WriteLine("WARNING: Database not connected, limited functionality");

        Boolean question = true;
        while (question)
        {

            String repsonse = Console.ReadLine();

                if (repsonse == "1")
                {
                    Console.WriteLine("All current contacts:");
                    sqlSelect(displayall);



                }
                else if (repsonse == "2")
                {
                    Console.WriteLine("Would you like to search by: ");
                    Console.WriteLine("1: FirstName");
                    Console.WriteLine("2: LastName");
                    Console.WriteLine("3: Address");
                    Console.WriteLine("4: City");
                    Console.WriteLine("5: State");
                    Console.WriteLine("6: Country");
                    Console.WriteLine("7: Zipcode");
                    Console.WriteLine("8: Area Code");
                    Console.WriteLine("9: Phone number");
                    Boolean tempBool = true;
                    while (tempBool)
                    {
                        String tempRes = Console.ReadLine();
                        if (tempRes == "1")
                        {
                            Console.WriteLine("Enter a FirstName:");
                            by = Console.ReadLine();

                            searchByFirst.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByFirst);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "2")
                        {
                            Console.WriteLine("Enter a LastName:");
                            by = Console.ReadLine();

                            searchByLast.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByLast);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }

                        }
                        else if (tempRes == "3")
                        {
                            Console.WriteLine("Enter an Address:");
                            by = Console.ReadLine();

                            searchByAddress.Parameters["@by"].Value = by;

                            try
                            {
                                sqlSelect(searchByAddress);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "4")
                        {
                            Console.WriteLine("Enter a City:");
                            by = Console.ReadLine();

                            searchByCity.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByCity);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "5")
                        {
                            Console.WriteLine("Enter a State:");
                            by = Console.ReadLine();

                            searchByState.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByState);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "6")
                        {
                            Console.WriteLine("Enter a Country:");
                            by = Console.ReadLine();

                            searchByCountry.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByCountry);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "7")
                        {
                            Console.WriteLine("Enter a Zipcode:");
                            by = Console.ReadLine();

                            searchByZip.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByZip);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "8")
                        {
                            Console.WriteLine("Enter an area code:");
                            by = Console.ReadLine();

                            searchByAreaCode.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByAreaCode);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else if (tempRes == "9")
                        {
                            Console.WriteLine("Enter a phone number:");
                            by = Console.ReadLine();

                            searchByPhone.Parameters["@by"].Value = by;
                            try
                            {
                                sqlSelect(searchByPhone);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not A valid entry! Returning to menu...");
                            tempBool = false;
                            break;
                        }
                    }
                }
                else if (repsonse == "3")
                {

                    Boolean tempBool = true;
                    Console.WriteLine("Would you like to Add or Update:");
                    Console.WriteLine("1: Add");
                    Console.WriteLine("2: Update");
                    while (tempBool)
                    {
                        String tempRes = Console.ReadLine();
                        if (tempRes == "1")
                        {
                            Console.WriteLine("Add contact information with the following format:");
                            Console.WriteLine("Skip fields if you do not have info available.");
                            Console.WriteLine("PiD is mandatory");
                            Console.WriteLine("PiD");
                            Console.WriteLine("FirstName");
                            Console.WriteLine("LastName");
                            Console.WriteLine("House Number");
                            Console.WriteLine("Street");
                            Console.WriteLine("City");
                            Console.WriteLine("State");
                            Console.WriteLine("Zipcode");
                            Console.WriteLine("Country");
                            Console.WriteLine("Phone country code");
                            Console.WriteLine("Phone area code");
                            Console.WriteLine("Phone number");
                            Console.WriteLine("Extension");
                            String pid = Console.ReadLine();
                            String frn = Console.ReadLine();
                            String ltn = Console.ReadLine();
                            String hsn = Console.ReadLine();
                            String str = Console.ReadLine();
                            String cty = Console.ReadLine();
                            String ste = Console.ReadLine();
                            String zip = Console.ReadLine();
                            String cry = Console.ReadLine();
                            String pcc = Console.ReadLine();
                            String pac = Console.ReadLine();
                            String phn = Console.ReadLine();
                            String ext = Console.ReadLine();

                            insertstuff.Parameters["@pid"].Value = pid;
                            insertstuff.Parameters["@fName"].Value = frn;
                            insertstuff.Parameters["@lname"].Value = ltn;
                            insertstuff.Parameters["@hnum"].Value = hsn;
                            insertstuff.Parameters["@street"].Value = str;
                            insertstuff.Parameters["@city"].Value = cty;
                            insertstuff.Parameters["@state"].Value = ste;
                            insertstuff.Parameters["@country"].Value = cry;
                            insertstuff.Parameters["@zip"].Value = zip;
                            insertstuff.Parameters["@countrycode"].Value = pcc;
                            insertstuff.Parameters["@area"].Value = pac;
                            insertstuff.Parameters["@number"].Value = phn;
                            insertstuff.Parameters["@ext"].Value = ext;

                            if (pid == "")
                            {
                                Console.WriteLine("PiD is mandatory!");
                                continue;
                            }
                            else if (frn == "")
                            {
                                Console.WriteLine("Your contact needs to have a name.");
                                continue;
                            }
                            try
                            {
                                myConnection.Open();
                                insertstuff.ExecuteNonQuery();
                                myConnection.Close();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Error: that primary key exists");
                                myConnection.Close();
                            }


                        }
                        else if (tempRes == "2")
                        {

                            Console.WriteLine("Enter the name or PID of the contact you want to update:");
                            Console.WriteLine("Leave unknown fields blank.\n\n");

                            Console.WriteLine("Format:");
                            Console.WriteLine("PID");
                            Console.WriteLine("First Name");
                            Console.WriteLine("Last Name");
                            String pid = Console.ReadLine();
                            String frn = Console.ReadLine();
                            String ltn = Console.ReadLine();
                            if (pid == "" & frn == "" & ltn == "")
                            {
                                Console.WriteLine("You need to put in information");
                                continue;
                            }
                            updateStuff.Parameters["@pid"].Value = pid;
                            
                            Console.WriteLine("Enter the new information for this contact:");
                            Console.WriteLine("House Number");
                            Console.WriteLine("Street");
                            Console.WriteLine("City");
                            Console.WriteLine("State");
                            Console.WriteLine("Zipcode");
                            Console.WriteLine("Country");
                            Console.WriteLine("Phone country code");
                            Console.WriteLine("Phone area code");
                            Console.WriteLine("Phone number");
                            Console.WriteLine("Extension");
                            String hsn = Console.ReadLine();
                            String str = Console.ReadLine();
                            String cty = Console.ReadLine();
                            String ste = Console.ReadLine();
                            String zip = Console.ReadLine();
                            String cry = Console.ReadLine();
                            String pcc = Console.ReadLine();
                            String pac = Console.ReadLine();
                            String phn = Console.ReadLine();
                            String ext = Console.ReadLine();
                            insertstuff.Parameters["@pid"].Value = pid;
                            
                            updateStuff.Parameters["@hnum"].Value = hsn;
                            updateStuff.Parameters["@street"].Value = str;
                            updateStuff.Parameters["@city"].Value = cty;
                            updateStuff.Parameters["@state"].Value = ste;
                            updateStuff.Parameters["@country"].Value = cry;
                            updateStuff.Parameters["@zip"].Value = zip;
                            updateStuff.Parameters["@countrycode"].Value = pcc;
                            updateStuff.Parameters["@area"].Value = pac;
                            updateStuff.Parameters["@number"].Value = phn;
                            updateStuff.Parameters["@ext"].Value = ext;
                            try
                            {
                                myConnection.Open();
                                updateStuff.ExecuteNonQuery();
                                myConnection.Close();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Exception. Potential SQL error, check database for inconsistencies...");
                            }

                            continue;


                        }
                        else
                        {
                            Console.WriteLine("Not A valid entry! Returning to menu...");
                            tempBool = false;
                            break;
                        }
                    }
                }
                else if (repsonse == "4")
                {

                    Console.WriteLine("Who would you like to remove:");
                    Console.WriteLine("Delete all contacts with these fields. Enter 0 to cancel.");
                    Console.WriteLine("Pid");
                    Console.WriteLine("FirstName");
                    Console.WriteLine("LastName");
                    String pid = Console.ReadLine();
                    String frn = Console.ReadLine();
                    String ltn = Console.ReadLine();
                    if (pid == "0" || frn == "0" || ltn == "0")
                    {
                        Console.WriteLine("returning to main menu...");
                        continue;
                    }
                    dropwhere.Parameters["@pid"].Value = pid;
                    try
                    {
                        myConnection.Open();

                        dropwhere.ExecuteNonQuery();
                        myConnection.Close();
                    }
                    catch (Exception )
                    {
                        myConnection.Close();
                    }
                    Console.WriteLine("Contact Deleted.");
                    continue;



                }
                else if (repsonse == "5")
                {
                    Console.WriteLine("Welcome to the Phone Contact App.");
                    Console.WriteLine("Please select one of the following options:");
                    Console.WriteLine("1: Display all contacts.");
                    Console.WriteLine("2: Search contacts.");
                    Console.WriteLine("3: Add or Update Contact.");
                    Console.WriteLine("4: Remove a Contact.");
                    Console.WriteLine("5: display menu");
                    Console.WriteLine("6: clear screen");
                    Console.WriteLine("7: Backup DatabaseData");
                    Console.WriteLine("8: View backup");
                    Console.WriteLine("9: Serialize Backup");
                    Console.WriteLine("0: Close The Application");
                    if (Notconnected)
                        Console.WriteLine("WARNING: Database not connected, limited functionality");
                }
                else if (repsonse == "6")
                {
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                }

                else if (repsonse == "7")
                {
                    //Person[] = new Person[] { };

                    try
                    {

                        SqlDataReader myReader = null;
                        File.WriteAllText("C:\\Users\\jackm\\Desktop\\Saved.txt", String.Empty);
                        myConnection.Open();
                        Console.Write("Results:\n");
                        myReader = displayall.ExecuteReader();
                        StreamWriter sw = new StreamWriter("C:\\Users\\jackm\\Desktop\\Saved.txt", true, Encoding.Unicode);
                       
                        while (myReader.Read())
                        {

                            sw.Write("PiD:: " + myReader["Pid"].ToString() + "\n");
                            sw.Write("     FirstName: " + myReader["FirstName"].ToString() + "\n");
                            sw.Write("     LastName: " + myReader["LastName"].ToString() + "\n");
                            sw.Write("     CountryCode: " + myReader["countryCode"].ToString() + "\n");
                            sw.Write("     AreaCode: " + myReader["areaCode"].ToString() + "\n");
                            sw.Write("     Number: " + myReader["number"].ToString() + "\n");
                            sw.Write("     Extension: " + myReader["ext"].ToString() + "\n");
                            sw.Write("     HouseNum: " + myReader["houseNum"].ToString() + "\n");
                            sw.Write("     Street: " + myReader["street"].ToString() + "\n");
                            sw.Write("     City: " + myReader["city"].ToString() + "\n");
                            sw.Write("     State: " + myReader["state"].ToString() + "\n");
                            sw.Write("     Country: " + myReader["country"].ToString() + "\n");
                            sw.Write("     Zipcode: " + myReader["zipcode"].ToString() + "\n");
                            sw.Write("\n\n\n");


                        }
                        sw.Close();

                        myConnection.Close();
                        Console.WriteLine("Write successful. Returning to menu.....");
                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }


                }
                else if (repsonse == "8")
                {
                    try
                    {
                        StreamReader sr = new StreamReader("C:\\Users\\jackm\\Desktop\\Saved.txt");
                        String line = sr.ReadLine();
                        while (line != null)
                        {
                            Console.WriteLine(line);
                            line = sr.ReadLine();

                        }
                        sr.Close();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error! File Not Found!");
                    }
                }

                else if (repsonse == "9")
                {
                    
                    try
                    {   
                        var data = File.ReadAllText("C:\\Users\\jackm\\Desktop\\Saved.txt");
                        File.WriteAllText("C:\\Users\\jackm\\Desktop\\Databackup.JSON", JsonConvert.SerializeObject(data));
                        
                    

                    }
                    catch (Exception )
                    {
                        Console.WriteLine("");
                    }
                    


                    Console.WriteLine("Data Serialized Successfuly...");


                }


                else if (repsonse == "0")
                {
                    question = false;
                    Console.WriteLine("closing....");

                    break;
                }

                else
                {
                    Console.WriteLine("That is not a valid response. Please give a valid response.");
                }
            

        }
        }
    }
}
