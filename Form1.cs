using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;

namespace FoodReco
{
    //min date 8 mar 2005
    //max date 5 jan 2013
    public partial class Form1 : Form
    {
        HashSet<String> foodItemList;
        HashSet<String> vegItemList;
        HashSet<String> nonVegItemList;
        HashSet<String> breakfastItemList;
        HashSet<String> lunchItemList;
        HashSet<String> dinnerItemList;
        HashSet<String> dessertItemList;
        HashSet<String> beverageItemList;
        HashSet<String> mexicanItemList;
        HashSet<String> italianItemList;
        HashSet<String> indianItemList;
        HashSet<String> frenchItemList;
        HashSet<String> chineseItemList;
        HashSet<String> continentalItemList;
        HashSet<String> thaiItemList;
        HashSet<String> businessIdList;
        
        public struct WordWithIndex
       {
        public String phraseName;
         public  int index;
          
       };
        public struct FoodItemWithPriority
        {
            public String foodItem;
            public double priority;

        };
        public struct DistanceDetails : IComparable<DistanceDetails>
        {
            public String businessId;
            public double distance;
            public int CompareTo(DistanceDetails d1)
            {
                
                if (d1.distance < this.distance)
                    return 1;
                else if (d1.distance > this.distance)
                    return -1;
                else
                    return 0;
            }
        };
        public struct RestaurantWithScore : IComparable<RestaurantWithScore>
        {
            public String restaurantName;
            public double score;
            public int CompareTo(RestaurantWithScore r1)
            {
                
                if (r1.score > this.score)
                    return 1;
                else if (r1.score < this.score)
                    return -1;
                else
                    return 0;
            }

        };
        public struct ScoreDetails
        {
            public double score;
            public double count;
        };
        public struct businessDetails
       {
           
           public String address;
           public String city;
           public String businessName;
           public int reviewCount;
           public double stars;
           public Dictionary<string,ScoreDetails> foodScores;
           public string businessId;

       };
        public struct reviewDetails
       {
           public int funny;
           public int cool;
           public int useful;
           public string userId;
           public string reviewId;
           public double stars;
           public DateTime date;
           public string reviewText;
           public string businessId;

       };
        
        Dictionary<string,businessDetails> businessDetailsList;
        Dictionary<string,double> phraseList;

        GenericTreeNode root;
        public int MINYEAR = 2005;
        public int MAXYEAR = 2013;
        public int MINREVIEWVOTES = -20;
        public int MAXREVIEWVOTES = 153;
        public int MINREVIEWCOUNTPERBUSINESS = 3;
        public int MAXREVIEWCOUNTPERBUSINESS = 803;
        public int MINSTARSPERBUSINESS = 1;
        public int MAXSTARSPERBUSINESS = 5;
        
        public Form1()
        {
            InitializeComponent();
            
            foodItemList= new HashSet<String>();
            vegItemList = new HashSet<String>();
            nonVegItemList = new HashSet<String>();
            breakfastItemList = new HashSet<String>();
            lunchItemList = new HashSet<String>();
            dinnerItemList = new HashSet<String>();
            dessertItemList = new HashSet<String>();
            beverageItemList = new HashSet<String>();
            mexicanItemList = new HashSet<String>();
            italianItemList = new HashSet<String>();
            indianItemList = new HashSet<String>();
            frenchItemList = new HashSet<String>();
            chineseItemList = new HashSet<String>();
            continentalItemList = new HashSet<String>();
            thaiItemList = new HashSet<String>();
            phraseList = new Dictionary<string,double>();
            businessDetailsList=new Dictionary<string,businessDetails>();
            businessIdList= new HashSet<String>();

            //populateBusinessDatabase();
            populateFoodItemList();
            populatePhraseList();
            populateBusinessList();
            //populateReviewDatabase();
            
            //mineReviews();
            //CommitScoreVector();
            populateBusinessVectors();
            buildQueryTree();
        }

        private void buildQueryTree()
        {
            root = new GenericTreeNode("root");
            int i;
            businessDetails busDet;
            GenericTreeNode cityNode,tempNode;
            for (i = 0; i < businessDetailsList.Count; i++)
            {
                busDet=businessDetailsList.ElementAt(i).Value;
                cityNode=root.searchForTreeNode(busDet.city);
                if (cityNode != null)
                {
                    cityNode.addNodeToChildList(busDet.businessId);
                }
                else
                {
                    root.addNodeToChildList(busDet.city);
                    cityNode = root.searchForTreeNode(busDet.city);
                    cityNode.addNodeToChildList(busDet.businessId);
                }
            }
            
        }

        private void populateBusinessVectors()
        {
            DataChannel channel = new DataChannel();
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();
            String query = "SELECT * FROM BusinessVectors";
            DataRow dr;
            int i,j;
            reviewDetails rD;
            string businessID;
            DataColumn dc;
            ScoreDetails sd;
            DataSet ds = executor.executeQuery(query, channel.GetDatabaseConnection);
            if (ds != null)
            {
                if (ds.Tables["Data"] != null)
                {
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        for (i = 0; i < ds.Tables["Data"].Rows.Count; i++)
                        {
                            dr = ds.Tables["Data"].Rows[i];
                            businessID = Convert.ToString(dr[0]);
                            for (j = 1; j < ds.Tables["Data"].Columns.Count; j++)
                            {
                                dc = ds.Tables["Data"].Columns[j];
                                sd.count = 0;
                                sd.score = Convert.ToDouble(dr[j]);
                                businessDetailsList[businessID].foodScores.Add(dc.ColumnName,sd);
                            }
                                
                        }
                    }
                }
            }

            channel.closeConn();
        }

        
        private void mineReviews()
        {
            DataChannel channel = new DataChannel();
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();
            String query = "SELECT * FROM Reviews";
            DataRow dr;
            int i;
            reviewDetails rD;
            DataSet ds = executor.executeQuery(query, channel.GetDatabaseConnection);
            if (ds != null)
            {
                if (ds.Tables["Data"] != null)
                {
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        for (i = 0; i < ds.Tables["Data"].Rows.Count; i++)
                        {
                            dr = ds.Tables["Data"].Rows[i];
                            rD.funny = Convert.ToInt32(dr[0]);
                            rD.cool = Convert.ToInt32(dr[1]);
                            rD.useful = Convert.ToInt32(dr[2]);
                            rD.userId = Convert.ToString(dr[3]);
                            rD.reviewId = Convert.ToString(dr[4]);
                            rD.stars = Convert.ToDouble(dr[5]);
                            rD.date = Convert.ToDateTime(dr[6]);
                            rD.reviewText = Convert.ToString(dr[7]);
                            rD.businessId = Convert.ToString(dr[8]);
                            processReview(rD);
                        }
                    }
                }
            }

            channel.closeConn();
        }

        private void processReview(reviewDetails rD)
        {
            List<WordWithIndex> currentPhraseList = new List<WordWithIndex>();
            List<WordWithIndex> currentFoodItemList = new List<WordWithIndex>();
            WordWithIndex p;
            List<WordWithIndex> currentWord = new List<WordWithIndex>(3);
            string temp,phraseWord;
            int i, j = 0, k1, k2, k3, flag = 0,index=0,indexWord=0, wordsMatched=1 ;
            
            i = 0;
            temp = getNextWord(rD.reviewText, i, out index);
            p.phraseName = temp;
            p.index = i;
            currentWord.Add(p);
            
            
            i = index;
            if (i >= rD.reviewText.Length)
                return;
                temp = getNextWord(rD.reviewText, i, out index);
                p.phraseName = temp;
                p.index = i;
                currentWord.Add(p);
            
            
            i = index;
            if (i >= rD.reviewText.Length)
                return;
            temp = getNextWord(rD.reviewText, i, out index);
            p.phraseName = temp;
            p.index = i;
            currentWord.Add(p);
            
            i = index;

            indexWord = 0;

            do
            {
                wordsMatched = 1;
                if (currentWord.Count==3 && phraseList.ContainsKey(currentWord[0].phraseName + " " + currentWord[1].phraseName + " " + currentWord[2].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName + " " + currentWord[1].phraseName + " " + currentWord[2].phraseName;
                    p.index = currentWord[0].index;
                    currentPhraseList.Add(p);
                    wordsMatched = 3;
                }
                else if (currentWord.Count >=2 && phraseList.ContainsKey(currentWord[0].phraseName + " " + currentWord[1].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName + " " + currentWord[1].phraseName;
                    p.index = currentWord[0].index;
                    currentPhraseList.Add(p);
                    wordsMatched = 2;
                }
                else if (currentWord.Count >= 1 && phraseList.ContainsKey(currentWord[0].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName;
                    p.index = currentWord[0].index;
                    currentPhraseList.Add(p);
                    wordsMatched = 1;
                }
                else if (currentWord.Count == 3 && foodItemList.Contains(currentWord[0].phraseName + " " + currentWord[1].phraseName + " " + currentWord[2].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName + " " + currentWord[1].phraseName + " " + currentWord[2].phraseName;
                    p.index = currentWord[0].index;
                    currentFoodItemList.Add(p);
                    wordsMatched = 3;
                }
                else if (currentWord.Count >= 2 && foodItemList.Contains(currentWord[0].phraseName + " " + currentWord[1].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName + " " + currentWord[1].phraseName;
                    p.index = currentWord[0].index;
                    currentFoodItemList.Add(p);
                    wordsMatched = 2;
                }
                else if (currentWord.Count >= 1 && foodItemList.Contains(currentWord[0].phraseName))
                {
                    p.phraseName = currentWord[0].phraseName;
                    p.index = currentWord[0].index;
                    currentFoodItemList.Add(p);
                    wordsMatched = 1;
                }
                
                for (k1 = 0; k1 < wordsMatched ; k1++)
                {
                    i = index;
                    currentWord.RemoveAt(0);
                    if (i < rD.reviewText.Length)
                    {
                        temp = getNextWord(rD.reviewText, i, out index);
                        p.phraseName = temp;
                        p.index = i;
                        currentWord.Add(p);
                    }
                }
            } while (i < rD.reviewText.Length || currentWord.Count>0);
            if (currentFoodItemList.Count == 0 || currentPhraseList.Count == 0)
                return;
            int phraseIndex=currentPhraseList.ElementAt(0).index, itemIndex=currentFoodItemList.ElementAt(0).index,nearestLeft,nearestRight;
            string nearestPhraseToLeft, nearestPhraseToRight;
            double phraseScore,newScore;
            ScoreDetails newScoreDetails;
            for (i = 0; i < currentFoodItemList.Count; i++)
            {
                nearestPhraseToLeft = findNearestPhraseToLeft(currentPhraseList, currentFoodItemList.ElementAt(i).index, out nearestLeft);
                nearestPhraseToRight = findNearestPhraseToRight(currentPhraseList, currentFoodItemList.ElementAt(i).index, out nearestRight);
                if (nearestLeft == -1 || nearestRight == -1)
                    phraseScore = (nearestRight != -1) ? phraseList[nearestPhraseToRight] : phraseList[nearestPhraseToLeft];
                else if (currentFoodItemList.ElementAt(i).index - nearestLeft > nearestRight - currentFoodItemList.ElementAt(i).index)
                    phraseScore = phraseList[nearestPhraseToRight];
                else if (currentFoodItemList.ElementAt(i).index - nearestLeft < nearestRight - currentFoodItemList.ElementAt(i).index)
                    phraseScore = phraseList[nearestPhraseToLeft];
                else
                    phraseScore = (phraseList[nearestPhraseToLeft] + phraseList[nearestPhraseToRight]) / 2;

                //DATE MANIPULATION
                phraseScore = phraseScore * (rD.date.Year - MINYEAR) / (MAXYEAR -MINYEAR);
                phraseScore = phraseScore * (rD.cool + rD.useful -rD.funny - MINREVIEWVOTES) / (MAXREVIEWVOTES - MINREVIEWVOTES);
                phraseScore = phraseScore * (businessDetailsList[rD.businessId].reviewCount - MINREVIEWCOUNTPERBUSINESS) / (MAXREVIEWCOUNTPERBUSINESS - MINREVIEWCOUNTPERBUSINESS);
                phraseScore = phraseScore * (businessDetailsList[rD.businessId].stars - MINSTARSPERBUSINESS) / (MAXSTARSPERBUSINESS - MINSTARSPERBUSINESS);
                if (businessDetailsList.ContainsKey(rD.businessId))
                {
                    if (businessDetailsList[rD.businessId].foodScores.ContainsKey(currentFoodItemList.ElementAt(i).phraseName))
                    {
                        newScoreDetails = businessDetailsList[rD.businessId].foodScores[currentFoodItemList.ElementAt(i).phraseName];
                        newScoreDetails.score = (newScoreDetails.score * newScoreDetails.count + phraseScore) / (newScoreDetails.count + 1);
                        newScoreDetails.count += 1;
                        businessDetailsList[rD.businessId].foodScores[currentFoodItemList.ElementAt(i).phraseName] = newScoreDetails;
                        //businessDetailsList[rD.businessId].foodScores.Add(currentFoodItemList.ElementAt(i).phraseName, newScoreDetails);
                    }
                    else
                    {
                        newScoreDetails.score = phraseScore;
                        newScoreDetails.count = 1;
                        businessDetailsList[rD.businessId].foodScores.Add(currentFoodItemList.ElementAt(i).phraseName, newScoreDetails);
                    }
                    
                    //INSERT INTO DATABSE HERE
                    //CommitScoreVector(businessDetailsList[rD.businessId],i);
                }
            }
        }

        private string findNearestPhraseToLeft(List<WordWithIndex> currentPhraseList, int index, out int nearestLeft)
        {
            
            int i;
            
            nearestLeft = currentPhraseList.ElementAt(0).index;

            if (nearestLeft >= index)
            {
                nearestLeft = -1;
                return "";
            }
            for (i = 1; i < currentPhraseList.Count; i++)
            {
                if (currentPhraseList.ElementAt(i).index >= index)
                    break;
                nearestLeft = currentPhraseList.ElementAt(i).index;
            }

            return currentPhraseList.ElementAt(i-1).phraseName;
        }
        private string findNearestPhraseToRight(List<WordWithIndex> currentPhraseList, int index, out int nearestRight)
        {

            int i;

            nearestRight = currentPhraseList.ElementAt(currentPhraseList.Count-1).index;

            if (nearestRight <= index)
            {
                nearestRight = -1;
                return "";
            }

            for (i = currentPhraseList.Count-2; i >= 0; i--)
            {
                if (currentPhraseList.ElementAt(i).index <= index)
                    break;
                nearestRight = currentPhraseList.ElementAt(i).index;
            }
            return currentPhraseList.ElementAt(i + 1).phraseName;
        }
        private string getNextWord(string reviewText, int i,out int index)
        {
            int j = reviewText.Length - 1;
            int k1 = reviewText.IndexOf(" ", i);
            if (k1 != -1)
                j = Math.Min(j, k1);
            k1 = reviewText.IndexOf(".", i);
            if (k1 != -1)
                j = Math.Min(j, k1);
            k1 = reviewText.IndexOf(",", i);
            if (k1 != -1)
                j = Math.Min(j, k1);
            k1 = reviewText.IndexOf(";", i);
            if (k1 != -1)
                j = Math.Min(j, k1);
            k1 = reviewText.IndexOf("!", i);
            if (k1 != -1)
                j = Math.Min(j, k1);
            if (j - i > 0)
            {
                index = j + 1;
                return reviewText.Substring(i, j - i).ToLower();
            }
            else
            {
                index = i+1;
                return "";
            }
        }
        private void populateBusinessDatabase()
        {
            var reader = new StreamReader(File.OpenRead("D:\\BITS Pilani\\4-1\\Advanced Data Mining\\ADM Project 2\\FoodReco\\FoodReco\\yelp_academic_dataset_business.json"));
            while (!reader.EndOfStream)
            {

                String line = reader.ReadLine();
                int x,index,y;

                if(line.Contains("\"Restaurants\"") || line.Contains("\"Food\""))
                {
                    x = line.IndexOf("business_id");
                    index = x + 15;
                    string businessId = line.Substring(index, 22);
    
                    x = line.IndexOf("full_address");
                    index = x + 16;
                    y = line.IndexOf("open");
                    string address = line.Substring(index, y-4-index);

                    x = line.IndexOf("city");
                    index = x + 8;
                    y = line.IndexOf("review_count");
                    string city = line.Substring(index, y-4-index);
                    
                    
                    x = y;
                    y = line.IndexOf("\"name\":");
                    int review_count = Convert.ToInt32(line.Substring(x + 15, y - 3 - (x + 15)+1));

                    x = y;
                    y = line.IndexOf("\"neighborhoods\":");
                    string business_name = line.Substring(x + 9, y - 4 - (x + 9) + 1);


                    x = line.IndexOf("\"stars\"");
                    double stars = Convert.ToDouble(line.Substring(x + 9, 3));
                    addBusinessId(businessId, address, city, review_count, business_name, stars);
                
                }            
            }
        }
        private void populateBusinessList()
        {
            DataChannel channel = new DataChannel();
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();
            businessDetails businessDetailsNode= new businessDetails();
            String query = "SELECT * FROM Business_Details";
            DataRow dr;
            int i,j;
            DataSet ds = executor.executeQuery(query, channel.GetDatabaseConnection);
            if (ds != null)
            {
                if (ds.Tables["Data"] != null)
                {
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        for (i = 0; i < ds.Tables["Data"].Rows.Count; i++)
                        {
                            dr = ds.Tables["Data"].Rows[i];
                            businessDetailsNode.businessId = Convert.ToString(dr[0]);
                            businessDetailsNode.address = Convert.ToString(dr[1]);
                            businessDetailsNode.city = Convert.ToString(dr[2]);
                            businessDetailsNode.reviewCount = Convert.ToInt32(dr[3]);
                            businessDetailsNode.businessName = Convert.ToString(dr[4]);
                            businessDetailsNode.stars = Convert.ToDouble(dr[5]);
                            businessDetailsNode.foodScores = new Dictionary<string, ScoreDetails>();
                            //for (j = 0; j < foodItemList.Count; j++)
                            //    businessDetailsNode.foodScores.Add(foodItemList.ElementAt(j),-100);
                               businessDetailsList.Add(Convert.ToString(dr[0]),businessDetailsNode);
                               businessIdList.Add(Convert.ToString(dr[0]));
                        }
                    }
                }
            }

            channel.closeConn();
        }
        private void populateReviewDatabase()
        {
            var reader = new StreamReader(File.OpenRead("D:\\BITS Pilani\\4-1\\Advanced Data Mining\\ADM Project 2\\FoodReco\\FoodReco\\yelp_academic_dataset_reviewTest.json"));
            while (!reader.EndOfStream)
            {

                String line = reader.ReadLine();

                int x = line.IndexOf("funny");
                int index = x + 8;
                int i = index;
                while (line[index] >= '0' && line[index] <= '9')
                    index++;
                string funnyScore = line.Substring(i, index - i);

                x = line.IndexOf("useful");
                index = x + 9;
                i = index;
                while (line[index] >= '0' && line[index] <= '9')
                    index++;
                string usefulScore = line.Substring(i, index - i);

                x = line.IndexOf("cool");
                index = x + 7;
                i = index;
                while (line[index] >= '0' && line[index] <= '9')
                    index++;
                string coolScore = line.Substring(i, index - i);

                x = line.IndexOf("user_id");
                index = x + 11;
                string userId = line.Substring(index, 22);

                x = line.IndexOf("review_id");
                index = x + 13;
                string reviewId = line.Substring(index, 22);

                x = line.IndexOf("stars");
                index = x + 8;
                i = index;
                while (line[index] >= '0' && line[index] <= '9')
                    index++;
                string stars = line.Substring(i, index - i);

                x = line.IndexOf("date");
                index = x + 8;
                string date = line.Substring(index, 10);


                x = line.IndexOf("text");
                index = x + 8;
                string reviewText = line.Substring(index, line.IndexOf("\", \"type\"") - index);

                x = line.IndexOf("business_id");
                index = x + 15;
                string businessId = line.Substring(index, 22);
                if(businessIdList.Contains(businessId))
                    addReview(Convert.ToInt32(funnyScore), Convert.ToInt32(usefulScore), Convert.ToInt32(coolScore),userId,reviewId,Convert.ToInt32(stars),Convert.ToDateTime(date),reviewText,businessId);
                
            }
            reader.Close();
        }


        /*
         * Function to Commit Score Vector to DB Table
         */
        private void CommitScoreVector()
        {
            // adds record into LOginDB
            DataChannel channel = new DataChannel();
            //string query = "Insert Into LoginDB (Name,Email,Password,ID_NO) Values (?,?,?,?)";
            bool isInsertquery = true;
            string query;
            int i,j;
            Dictionary<string,ScoreDetails> foodScores;
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();
            DataSet ds;
            for (i = 0; i < businessDetailsList.Count; i++)
            {
                if (businessDetailsList.ElementAt(i).Value.foodScores.Count > 0)
                {
                    query = "Update BusinessVectors set ";
                    foodScores=businessDetailsList.ElementAt(i).Value.foodScores;
                    for (j = 0; j < foodScores.Count-1; j++)
                    {
                        query += foodScores.ElementAt(j).Key + "= "+foodScores.ElementAt(j).Value.score + ", ";

                    }
                    query += foodScores.ElementAt(j).Key + "= " + foodScores.ElementAt(j).Value.score + " where Business_ID= '" + businessDetailsList.ElementAt(i).Key + "';";
                      ds = executor.executeQuery(query, channel.GetDatabaseConnection);

                }
            }
     }     

        private void addReview(int funny,int useful,int cool,string user_id, string review_id,int stars,DateTime date,string reviewText,string business_id)
        {
            // adds record into LOginDB
            DataChannel channel = new DataChannel();
            //string query = "Insert Into LoginDB (Name,Email,Password,ID_NO) Values (?,?,?,?)";
            string query = "Insert Into Reviews (Funny,Useful,Cool,User_ID,Review_ID,Stars,Review_Date,Review_Text,Business_ID) Values (?,?,?,?,?,?,?,?,?)";

            using (channel.GetDatabaseConnection)
            {
                using (OdbcCommand cmd = new OdbcCommand(query, channel.GetDatabaseConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("Funny", OdbcType.Text);
                    cmd.Parameters["Funny"].Value = funny;
                    cmd.Parameters.AddWithValue("Useful", useful);
                    cmd.Parameters.AddWithValue("Cool", cool);
                    cmd.Parameters.AddWithValue("User_ID", user_id);
                    cmd.Parameters.AddWithValue("Review_ID", review_id);
                    cmd.Parameters.AddWithValue("Stars", stars);
                    cmd.Parameters.AddWithValue("Review_Date", date);
                    cmd.Parameters.Add("Review_Text", OdbcType.Text);
                    cmd.Parameters["Review_Text"].Value = reviewText;
                    //cmd.Parameters.AddWithValue("Review_Text", reviewText.Substring(0,255));
                    cmd.Parameters.AddWithValue("Business_ID", business_id);


                    channel.GetDatabaseConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void addBusinessId(string businessId, string address, string city, int review_count, string business_name, double stars)
        {
            // adds record into LOginDB
            DataChannel channel = new DataChannel();
            //string query = "Insert Into LoginDB (Name,Email,Password,ID_NO) Values (?,?,?,?)";
            string query = "Insert Into Business_Details (Business_ID,Address, City, Review_Count, Business_Name, Stars) Values (?,?,?,?,?,?)";

            using (channel.GetDatabaseConnection)
            {
                using (OdbcCommand cmd = new OdbcCommand(query, channel.GetDatabaseConnection))
                {
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.Parameters.AddWithValue("Business_ID", businessId);
                    cmd.Parameters.AddWithValue("Address", address);
                    cmd.Parameters.AddWithValue("City", city);
                    cmd.Parameters.AddWithValue("Review_Count", review_count);
                    cmd.Parameters.AddWithValue("Business_Name", business_name);
                    cmd.Parameters.AddWithValue("Stars", stars);

                    channel.GetDatabaseConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void populateFoodItemList()
        {
            DataChannel channel = new DataChannel();
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();

            String query = "SELECT *  FROM Food_Items";
            DataSet ds = executor.executeQuery(query, channel.GetDatabaseConnection);
            int i;
            if (ds != null)
            {
                if (ds.Tables["Data"] != null)
                {
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        for (i = 0; i < ds.Tables["Data"].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables["Data"].Rows[i];
                            foodItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[1]))
                                vegItemList.Add(Convert.ToString(dr[0]));
                            else
                                nonVegItemList.Add(Convert.ToString(dr[0]));

                            if (Convert.ToBoolean(dr[2]))
                                breakfastItemList.Add(Convert.ToString(dr[0]));

                            if (Convert.ToBoolean(dr[3]))
                                lunchItemList.Add(Convert.ToString(dr[0]));

                            if (Convert.ToBoolean(dr[4]))
                                dinnerItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[5]))
                                beverageItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[6]))
                                dessertItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[7]))
                                indianItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[8]))
                                mexicanItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[9]))
                                chineseItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[10]))
                                italianItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[11]))
                                continentalItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[11]))
                                continentalItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[12]))
                                thaiItemList.Add(Convert.ToString(dr[0]));
                            if (Convert.ToBoolean(dr[13]))
                                frenchItemList.Add(Convert.ToString(dr[0]));
                        }
                    }
                }
            }

            channel.closeConn();
        }
        private void populatePhraseList()
        {
            DataChannel channel = new DataChannel();
            DatabaseOpsExecutor executor = new DatabaseOpsExecutor();

            String query = "SELECT *  FROM Word_Scores";
            DataSet ds = executor.executeQuery(query, channel.GetDatabaseConnection);
            //WordWithIndex p;
            int i;
            if (ds != null)
            {
                if (ds.Tables["Data"] != null)
                {
                    if (ds.Tables["Data"].Rows.Count > 0)
                    {
                        for (i = 0; i < ds.Tables["Data"].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables["Data"].Rows[i];
                            //p.phraseName = Convert.ToString(dr[0]);
                            //p.score = Convert.ToDouble(dr[1]);
                            phraseList.Add(Convert.ToString(dr[0]), Convert.ToDouble(dr[1]));
                        }
                    }
                }
            }

            channel.closeConn();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            
            getRecommendations(FoodItem1TextBox.Text,LocationTextBox.Text);
        }

        private void getRecommendations(string foodItem, string location)
        {
            GenericTreeNode cityNode = root.searchForTreeNode(location);
            List<FoodItemWithPriority> userFoodItems = new List<FoodItemWithPriority>();
            FoodItemWithPriority fp;
            int i, j, itemFoundCount = 0, goodScoreItemCount = 0;
            //Dictionary<string, List<RestaurantWithScore>> foodRecommendations= new Dictionary<string,List<RestaurantWithScore>>();
            //foodRecommendations = getSimilarItems(foodItem);
            ScoreDetails sD;
            HashSet<string> similarFoodItemList = getSimilarItems();
            Dictionary<string, ScoreDetails> foodVectorWithoutSimilarItems = new Dictionary<string, ScoreDetails>();
            Dictionary<string, ScoreDetails> foodVectorWithSimilarItemsWithHighScore = new Dictionary<string, ScoreDetails>();
            Dictionary<string, ScoreDetails> foodVectorWithSimilarItemsWithAvgScore = new Dictionary<string, ScoreDetails>();

            if (!FoodItem1TextBox.Text.Equals(string.Empty))
            {
                fp.foodItem = FoodItem1TextBox.Text;
                fp.priority = 2;
                userFoodItems.Add(fp);
            }
            if (!FoodItem2TextBox.Text.Equals(string.Empty))
            {
                fp.foodItem=FoodItem2TextBox.Text;
                fp.priority=1.75;
                userFoodItems.Add( fp);
            }
            if (!FoodItem3TextBox.Text.Equals(string.Empty))
            {
                fp.foodItem=FoodItem3TextBox.Text;
                fp.priority=1.5;
                userFoodItems.Add( fp);
            }
            if (!FoodItem4TextBox.Text.Equals(string.Empty))
            {
                fp.foodItem=FoodItem4TextBox.Text;
                fp.priority=1.25;
                userFoodItems.Add( fp);
            }
            if (!FoodItem5TextBox.Text.Equals(string.Empty))
            {
                fp.foodItem=FoodItem5TextBox.Text;
                fp.priority=1;
                userFoodItems.Add( fp);
            }

            for (i = 0; i < foodItemList.Count; i++)
            {
                sD.count = 1;
                sD.score = similarFoodItemList.Contains(foodItemList.ElementAt(i)) ? 4 : -100;
                foodVectorWithSimilarItemsWithHighScore.Add(foodItemList.ElementAt(i), sD);
                
                sD.score = similarFoodItemList.Contains(foodItemList.ElementAt(i)) ? 0.5 : -100;
                foodVectorWithSimilarItemsWithAvgScore.Add(foodItemList.ElementAt(i), sD);
                
                sD.score = -100;
                foodVectorWithoutSimilarItems.Add(foodItemList.ElementAt(i), sD);
            }
            for (i = 0; i < userFoodItems.Count; i++)
            {
                if (foodVectorWithoutSimilarItems.ContainsKey(userFoodItems.ElementAt(i).foodItem))
                {
                    sD.count = 1;
                    sD.score = 4;
                    foodVectorWithoutSimilarItems[userFoodItems.ElementAt(i).foodItem] = sD;
                }
            }

            List<DistanceDetails> distanceVector;
            
            Dictionary<string, ScoreDetails> foodScore;
            List<RestaurantWithScore> recommendationList= new List<RestaurantWithScore>();
            RestaurantWithScore rstTemp;
            int minItemCount = (userFoodItems.Count == 1) ? 1 : userFoodItems.Count ;
            int minGoodScoreItemCount = (userFoodItems.Count == 1) ? 1 : Convert.ToInt32(Math.Ceiling(userFoodItems.Count*0.4));
            double score;
            string scoreString;
            if (cityNode != null)
            {
                foreach (GenericTreeNode current in cityNode.childList)
                {
                    foodScore = businessDetailsList[current.data].foodScores;
                    itemFoundCount = 0;
                    goodScoreItemCount = 0;
                    score = 0;
                    for (i = 0; i < userFoodItems.Count; i++)
                    {
                        if (foodScore.ContainsKey(userFoodItems.ElementAt(i).foodItem))
                        {
                            if (foodScore[userFoodItems.ElementAt(i).foodItem].score > 0)
                            {
                                //score=(score*itemFoundCount + userFoodItems.ElementAt(i).priority* foodScore[userFoodItems.ElementAt(i).foodItem].score)/(itemFoundCount+1);
                                score = (score * itemFoundCount +  foodScore[userFoodItems.ElementAt(i).foodItem].score) / (itemFoundCount + 1);
                                itemFoundCount++;
                                //if (foodScore[userFoodItems.ElementAt(i).foodItem].score >= 1)
                                    goodScoreItemCount++ ;  
                            }
                            
                        }
                        
                    }
                    if (minGoodScoreItemCount <= goodScoreItemCount && minItemCount <= itemFoundCount)
                    {
                        rstTemp.score = score;
                        rstTemp.restaurantName = businessDetailsList[current.data].businessName;
                        recommendationList.Add(rstTemp);
                    }
                    


                }
                recommendationList.Sort();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                for (i = 0; i < recommendationList.Count; i++)
                {
                    scoreString=Convert.ToString( recommendationList.ElementAt(i).score);
                    scoreString=scoreString.Substring(0,Convert.ToInt32( Math.Min(4,scoreString.Length)));
                    listBox1.Items.Add(recommendationList.ElementAt(i).restaurantName +"    "+scoreString );
                }
                distanceVector = findKNearestNeighbour(foodVectorWithoutSimilarItems,cityNode);
                distanceVector.Sort();
                for (i = 0; i < distanceVector.Count; i++)
                {
                    scoreString = Convert.ToString(distanceVector.ElementAt(i).distance);
                    listBox2.Items.Add(businessDetailsList[distanceVector.ElementAt(i).businessId].businessName + "    " + scoreString);
                }

                //listbox3
                distanceVector = findKNearestNeighbour(foodVectorWithSimilarItemsWithHighScore, cityNode);
                distanceVector.Sort();
                for (i = 0; i < distanceVector.Count; i++)
                {
                    scoreString = Convert.ToString(distanceVector.ElementAt(i).distance);
                    listBox3.Items.Add(businessDetailsList[distanceVector.ElementAt(i).businessId].businessName + "    " + scoreString);
                }

                //listbox4
                distanceVector = findKNearestNeighbour(foodVectorWithSimilarItemsWithAvgScore, cityNode);
                distanceVector.Sort();
                for (i = 0; i < distanceVector.Count; i++)
                {
                    scoreString = Convert.ToString(distanceVector.ElementAt(i).distance);
                    listBox4.Items.Add(businessDetailsList[distanceVector.ElementAt(i).businessId].businessName + "    " + scoreString);
                }
            }
            //temp.Sort();
            //for(int pp=0;pp<)
        }
        
        private List<DistanceDetails> findKNearestNeighbour(Dictionary<string, ScoreDetails> foodVector, GenericTreeNode cityNode)
        {
            int i;
            DistanceDetails tempNode;
            List<DistanceDetails> distanceVector = new List<DistanceDetails>();
            for (i = 0; i < businessDetailsList.Count; i++)
            {
                if (cityNode.searchForTreeNode(businessDetailsList.ElementAt(i).Value.businessId) != null)
                {
                    tempNode.distance = computeDistance(businessDetailsList.ElementAt(i).Value.foodScores, foodVector);
                    tempNode.businessId = businessDetailsList.ElementAt(i).Value.businessId;
                    distanceVector.Add(tempNode);
                }
            }
            return distanceVector;
        }

        private double computeDistance(Dictionary<string, ScoreDetails> foodVector_1, Dictionary<string, ScoreDetails> foodVector_2)
        {
            int i;
            double distance=0;
            for (i = 0; i < foodVector_1.Count; i++)
            {
                if(foodVector_2.ElementAt(i).Value.score > -100)
                    distance += Math.Pow((foodVector_1.ElementAt(i).Value.score - foodVector_2.ElementAt(i).Value.score), 2);
            }
            
            return Math.Sqrt( distance);
        }

        private HashSet<string> getSimilarItems()
        {
            HashSet<string> foodRecommendations = new HashSet<string>();

            HashSet<string> tempCuisineList = new HashSet<string>();
            HashSet<string> tempVegNonVegList = new HashSet<string>();
            int i;
            for (i = 0; i < MealTypeList.CheckedIndices.Count; i++)
            {
                if (MealTypeList.CheckedIndices[i] == 0)
                    foodRecommendations = updateFoodRecommendations(foodRecommendations, breakfastItemList);
                else if(MealTypeList.CheckedIndices[i] == 1)
                    foodRecommendations = updateFoodRecommendations(foodRecommendations, lunchItemList);
                else
                    foodRecommendations = updateFoodRecommendations(foodRecommendations, dinnerItemList);
            }
            
            for (i = 0; i < VegNonVegList.CheckedIndices.Count; i++)
            {
                if (VegNonVegList.CheckedIndices[i] == 0)
                    tempVegNonVegList = updateFoodRecommendations(tempVegNonVegList, vegItemList);
                else if (VegNonVegList.CheckedIndices[i] == 1)
                    tempVegNonVegList = updateFoodRecommendations(tempVegNonVegList, nonVegItemList);
            }

            foodRecommendations = updateFoodRecommendationsIntersection(foodRecommendations,tempVegNonVegList);
            
            for (i = 0; i < cuisineList.CheckedIndices.Count; i++)
            {
                if (cuisineList.CheckedIndices[i] == 0)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, chineseItemList);
                else if (cuisineList.CheckedIndices[i] == 1)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, italianItemList);
                else if (cuisineList.CheckedIndices[i] == 2)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, indianItemList);
                else if (cuisineList.CheckedIndices[i] == 3)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, frenchItemList);
                else if (cuisineList.CheckedIndices[i] == 4)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, thaiItemList);
                else if (cuisineList.CheckedIndices[i] == 5)
                    tempCuisineList = updateFoodRecommendations(tempCuisineList, continentalItemList);
            }
            foodRecommendations = updateFoodRecommendationsIntersection(foodRecommendations, tempCuisineList);
            
                return foodRecommendations;
        }

        private HashSet<string> updateFoodRecommendationsIntersection(HashSet<string> list_1, HashSet<string> list_2)
        {
            int i;
            HashSet<string> foodRecommendations = new HashSet<string>();
            for (i = 0; i < list_1.Count; i++)
            {
                if (list_2.Contains(list_1.ElementAt(i)))
                    foodRecommendations.Add(list_1.ElementAt(i));
            }
            return foodRecommendations;
        }

        private HashSet<string> updateFoodRecommendations(HashSet<string> foodRecommendations, HashSet<string> ItemList)
        {
            int i;
            for (i = 0; i < ItemList.Count; i++)
            {
                if(!foodRecommendations.Contains(ItemList.ElementAt(i)))
                    foodRecommendations.Add(ItemList.ElementAt(i));
            }
            return foodRecommendations;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       


       

    }

    
}
