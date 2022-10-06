using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using vlapp.Models;

namespace vlapp.Control
{
    internal class Database_Functions
    {
        public static MySqlConnection con;
        public static string connectDb() {
            string connected = "not connected";
            try
            {
                string cs = @"server=192.168.1.71;userid=testpro;password=1234567890;database=testpro";
                con = new MySqlConnection(cs);
                openDb(con);
                //string version = con.ServerVersion;
                connected = "connected";
                closeDb(con); 
            }
            catch (Exception)
            {
                connected = "not connected";
            }
            return connected;
        }

        public static string closeDb(MySqlConnection exCon) {
            try
            {
                exCon.Close();
                return "db closed";
            }
            catch (Exception)
            {
                return "db failed to close";
            }
        }

        public static string openDb(MySqlConnection exCon) {
            try
            {
                exCon.Open();
                return "db is opened";
            }
            catch (Exception)
            {
                return "db failed to open";
            }
        }

        MySqlDataReader excecuteQuery(string query) {
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader;
            closeDb(con);
            openDb(con);
            reader = command.ExecuteReader();
            return reader;
        }

        long executeQueryWithReturnId(string query) {
            MySqlCommand command = new MySqlCommand(query, con);
            MySqlDataReader reader;
            closeDb(con);
            openDb(con);
            reader = command.ExecuteReader();
            long lastId = command.LastInsertedId;
            return lastId;
        }

        public long insertData(string tbl, string[] fields, string[] values) {
            if (fields.Length != values.Length)
            {
                return 0;
            }
            else {
                try
                {
                    //string query = "INSERT INTO tbl_node(ip,blind_num) VALUES('192.168.1.1','1');";
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("INSERT INTO ");
                    sbQuery.Append(tbl);
                    sbQuery.Append('(');
                    for (int x = 0; x < fields.Length; x++)
                    {
                        sbQuery.Append(fields[x]);
                        if (x <= fields.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(") VALUES(");
                    for (int y = 0; y < values.Length; y++)
                    {
                        sbQuery.Append('\u0027'); //apastrophe
                        sbQuery.Append(values[y]);
                        sbQuery.Append('\u0027'); //apastrophe
                        if (y <= values.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(");");

                    long lastId = executeQueryWithReturnId(sbQuery.ToString());
                    closeDb(con);
                    return lastId;
                }
                catch (Exception)
                {
                    //if 0 is returned that means saving is failed
                    return 0;
                }
            }
        }

        public long updateData(string tbl, string[] fields, string[] values, string[] fCondition, string[] vCondition)
        {
            if (fields.Length != values.Length)
            {
                return 0;
            }
            else if (fCondition.Length != vCondition.Length)
            {
                return 0;
            }
            else {
                try
                {
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("UPDATE ");
                    sbQuery.Append(tbl);
                    sbQuery.Append(" SET ");
                    for (int x = 0; x < fields.Length; x++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fields[x]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append('=');
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(values[x]);
                        sbQuery.Append('\u0022'); //qutation
                        if (x <= values.Length - 2)
                        {
                            sbQuery.Append('\u002C'); //comma
                        }
                    }
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(" = ");
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                    sbQuery.Append(';');

                    long lastId = executeQueryWithReturnId(sbQuery.ToString());
                    closeDb(con);
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public string deleteData(string tbl, string[] fCondition, string[] vCondition)
        {
            if (fCondition.Length != vCondition.Length)
            {
                return "condition field and condition values musbt be the same count";
            }
            else
            {
                try
                {
                    StringBuilder sbQuery = new StringBuilder();
                    sbQuery.Append("DELETE ");
                    sbQuery.Append(" FROM ");
                    sbQuery.Append(tbl);
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++)
                    {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(" = ");
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                    sbQuery.Append(';');

                    MySqlDataReader reader = excecuteQuery(sbQuery.ToString());
                    closeDb(con);
                    return "deleted successfully";
                }
                catch (Exception)
                {
                    return "deleting failed";
                }
            }
        }

        public string clearTbl(string tbl) {
            try
            {
                string query = "DELETE FROM `"+tbl+"`;";
                MySqlDataReader reader = excecuteQuery(query.ToString());
                closeDb(con);
                return "db cleared";
            }
            catch (Exception)
            {
                return "db failed to clear";
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT * ");
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl, string[] fields)
        {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT ");
                for (int x = 0; x < fields.Length; x++)
                {
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    sbQuery.Append(fields[x]);
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    if (x <= fields.Length - 2)
                    {
                        sbQuery.Append('\u002C'); //comma
                    }
                }
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        ////collectData must be use like this////
        //MySqlDataReader insert = df.collectData(tbl);
        //while (insert.Read()) {
        //    MessageBox.Show(insert.GetString(0)+ " " +insert.GetString(1)+ " " +insert.GetString(2));
        //}
        //Database_Functions.closeDb(Database_Functions.con);
        public MySqlDataReader collectData(string tbl, string[] fields, string[] fCondition, string[] vCondition) {
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT ");
                for (int x=0; x < fields.Length; x++) {
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    sbQuery.Append(fields[x]);
                    sbQuery.Append('\u0060'); //GRAVE ACCENT 
                    if (x <= fields.Length - 2)
                    {
                        sbQuery.Append('\u002C'); //comma
                    }
                }
                sbQuery.Append(" FROM ");
                sbQuery.Append(tbl);

                if (fCondition.Length != 0) {
                    sbQuery.Append(" WHERE ");
                    for (int y = 0; y < fCondition.Length; y++) {
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append(fCondition[y]);
                        sbQuery.Append('\u0060'); //GRAVE ACCENT 
                        sbQuery.Append('=');
                        sbQuery.Append('\u0022'); //qutation
                        sbQuery.Append(vCondition[y]);
                        sbQuery.Append('\u0022'); //qutation
                        if (y <= fCondition.Length - 2)
                        {
                            sbQuery.Append(" AND ");
                        }
                    }
                }
                sbQuery.Append(';');

                return excecuteQuery(sbQuery.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public long blindConfig(string ip, int blindId, int redVal, int greenVal, int blueVal, int currVal)
        {
            string tbl = "tbl_blind";
            string[] fields = { "blind_index", "red_val", "green_val", "blue_val", "current_gain", "ip" };
            string[] values = { blindId.ToString(), redVal.ToString(), greenVal.ToString(), blueVal.ToString(), currVal.ToString(), ip };
            string[] fCondition = { fields[0], fields[5] };
            string[] vCondition = { values[0], values[5] };
            int cnt = 0;
            MySqlDataReader collection = collectData(tbl, fields, fCondition, vCondition);
            if (collection == null) 
            { 
                return 0; 
            }

            while (collection.Read()) { 
                cnt++;
            }

            if (cnt == 0)
            {
                return insertData(tbl, fields, values);
            }
            else 
            {
                //data is existing
                updateData(tbl, fields, values, fCondition, vCondition);
                return 9999;
            }
        }

        public int resetBlindConfig(string ip, int blindId) {
            string tbl = "tbl_blind";
            string tbl_2 = "tbl_module";
            string[] fields = { "red_val", "green_val", "blue_val", "current_gain" };
            string[] values = { "0", "0", "0", "0" };
            string[] fCondition = { "ip", "blind_index" };
            string[] vCondition = { ip, blindId.ToString() };

            long bTbl = updateData(tbl, fields, values, fCondition, vCondition);
            if (bTbl == 0) 
            {
                return 0;
            }
            long mTbl = updateData(tbl_2, fields, values, fCondition, vCondition);
            if (mTbl == 0) 
            {
                return 0;
            }
            return 1;
        }

        public long moduleConfig(string ip, int blindId, int moduleId, int redVal, int greenVal, int blueVal, int currVal) 
        {
            string tbl = "tbl_module";
            string[] fields = { "blind_index", "module_index", "red_val", "green_val", "blue_val", "current_gain", "ip" };
            string[] values = { blindId.ToString(), moduleId.ToString(), redVal.ToString(), greenVal.ToString(), blueVal.ToString(), currVal.ToString(), ip };
            string[] fCondition = { fields[0], fields[1], fields[6] };
            string[] vCondition = { values[0], values[1], values[6] };
            int cnt = 0;
            MySqlDataReader collection = collectData(tbl, fields, fCondition, vCondition);
            if (collection == null)
            {
                return 0;
            }
            while (collection.Read()) 
            {
                cnt++;
            }

            if (cnt == 0)
            {
                return insertData(tbl, fields, values);
            }
            else 
            {
                //data is existing
                updateData(tbl, fields, values, fCondition, vCondition);
                return 9999;        
            }
        }

        public long saveSchedule(string title , string startDate , string endDate)
        {
            string tbl = "tbl_schedule";
            string[] fields = { "title", "start_date", "end_date" };
            string[] values = { title, startDate, endDate };
            return insertData(tbl, fields, values);
        }


        public void saveTime(string start_time , string end_time , int day , int sched_id)
        {
            string tbl = "tbl_time";
            string[] fields = { "start_time", "end_time", "day" , "schedule_id"};
            string[] values = {start_time , end_time , day.ToString() , sched_id.ToString() };
            insertData(tbl , fields , values);
        }

        public List<VideoListItem> getSchedule()
        {
            List<VideoListItem> schedList = new List<VideoListItem>();
            MySqlDataReader readSched = collectData("tbl_schedule");
            if (readSched != null)
            {
                while (readSched.Read())
                {
                    schedList.Add(new VideoListItem() { id = readSched.GetInt32(0), title = readSched.GetString(1), tfrom = readSched.GetString(2), tto = readSched.GetString(3) });
                }
                return schedList;
            }
            else
            {
                return null;
            }
        }

        public List<BlindItem> getBlindProps(string ip) { 
            List<BlindItem> blindList = new List<BlindItem>();
            string tbl = "tbl_blind";
            string[] fields = { "blind_index", "red_val", "green_val", "blue_val", "current_gain" };
            string[] fCondition = { "ip" };
            string[] vCondition = { ip };
            MySqlDataReader readBlind = collectData(tbl, fields, fCondition, vCondition);
            if (readBlind != null)
            {
                while (readBlind.Read())
                {
                    blindList.Add(new BlindItem() { index = readBlind.GetInt32(0), redVal = readBlind.GetInt32(1), greenVal = readBlind.GetInt32(2), blueVal = readBlind.GetInt32(3), currentGain = readBlind.GetInt32(4) });
                }
                return blindList;
            }
            else
            {
                return null;            
            }
        }

        public List<ModuleItem> getModuleProps(string ip, int blindIndex) { 
            List <ModuleItem> moduleList = new List<ModuleItem>();
            string tbl = "tbl_module";
            string[] fields = { "blind_index", "module_index", "red_val", "green_val", "blue_val", "current_gain" };
            string[] fCondition = { "ip", "blind_index" };
            string[] vCondition = { ip, blindIndex.ToString() };
            MySqlDataReader readModule = collectData(tbl, fields, fCondition, vCondition);
            if (readModule != null)
            {
                while (readModule.Read())
                {
                    moduleList.Add(new ModuleItem() { blindIndex = readModule.GetInt32(0), moduleIndex = readModule.GetInt32(1), redVal = readModule.GetInt32(2), greenVal = readModule.GetInt32(3), blueVal = readModule.GetInt32(4), currentGain = readModule.GetInt32(5) });
                }
                return moduleList;
            }
            else
            {
                return null;            
            }
        }

        public List<TimeListItem> getTimeListOf(int sched_id)
        {
            List<TimeListItem> TimeList = new List<TimeListItem>();
            string tbl = "tbl_time";
            string[] fields = { "start_time" , "end_time" , "day" , "schedule_id" };
            string[] fCondition = { "schedule_id" };
            string[] vCondition = { sched_id.ToString() };
            MySqlDataReader readTime = collectData(tbl, fields, fCondition, vCondition);
            if (readTime != null)
            {
                while (readTime.Read())
                {
                    TimeList.Add(new TimeListItem() { from_time = readTime.GetString(0) , to_time= readTime.GetString(1), day = readTime.GetInt32(2) , schedule_id = readTime.GetInt32(3) });
                }
                return TimeList;
            }
            else
            {
                return null;
            }
        }


        public long scheduleUpdate(string title, string from , string to, int id)
        {
            string tbl = "tbl_schedule";
            string[] fields = { "title" , "start_date" , "end_date" };
            string[] values = { title , from , to  };
            string[] fCondition = { "id" };
            string[] vCondition = { id.ToString() };
            int cnt = 0;

            //data is existing
            updateData(tbl, fields, values, fCondition, vCondition);
            return 9999;
        }


        public long updateTime(string fTime , string eTime , int day , int sched_id)
        {
            string tbl = "tbl_time";
            string[] fields = { "start_time" , "end_time" , "day" , "schedule_id" };
            string[] values = { fTime, eTime, day.ToString(), sched_id.ToString() };
            string[] fCondition = { "schedule_id" , "day" };
            string[] vCondition = { sched_id.ToString(), day.ToString() };
            int cnt = 0;
            MySqlDataReader collection = collectData(tbl, fields, fCondition, vCondition);
            if (collection == null)
            {
                return 0;
            }
            while (collection.Read())
            {
                cnt++;
            }

            if (cnt == 0)
            {
                return insertData(tbl, fields, values);
            }
            else
            {
                //data is existing
                updateData(tbl, fields, values, fCondition, vCondition);
                return 9999;
            }
        }

        public string deleteSched(int sched_id)
        {
            string tbl = "tbl_schedule";
            string[] fCondition = { "id" };
            string[] vCondition = { sched_id.ToString() };

            return deleteData(tbl, fCondition, vCondition);

        }

        public string deleteDays( int sched_id)
        {
            string tbl = "tbl_time";
            string[] fCondition = { "schedule_id" };
            string[] vCondition = { sched_id.ToString()};

            return deleteData(tbl, fCondition, vCondition);

        }
            public string deleteDays(int day , int sched_id)
        {
            string tbl = "tbl_time";
            string[] fCondition = { "schedule_id" , "day" };
            string[] vCondition = { sched_id.ToString() , day.ToString() };

            return deleteData(tbl, fCondition, vCondition);
            
        }

    }
}
