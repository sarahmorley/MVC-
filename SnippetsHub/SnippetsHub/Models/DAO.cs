 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;



namespace SnippetsHub.Models
{
    public class DAO
    {

        SqlConnection conn;
        public string message = "";

        public void Connection()
        {
            string membersCon =
                WebConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            conn = new SqlConnection(membersCon);
        }

        public int Insert(Member member)
        {
            int count = 0;
            Connection();
            SqlCommand cmd = new SqlCommand("Insert_Into_tbMember", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //adding parameters with value
            cmd.Parameters.AddWithValue("@first", member.FirstName);
            cmd.Parameters.AddWithValue("@last", member.LastName);
            cmd.Parameters.AddWithValue("@email", member.Email);
            cmd.Parameters.AddWithValue("@pass", member.Password);
            cmd.Parameters.AddWithValue("username", member.Username);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {

                conn.Close();
            }

            return count;
        }
        public string CheckLogin(Member member)
        {
            

            Connection();
            SqlCommand cmd = new SqlCommand("CheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", member.Email);
            cmd.Parameters.AddWithValue("@pass", member.Password);
            try
            {
                conn.Open();
                member.FirstName = (string)cmd.ExecuteScalar();
             
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return member.FirstName;
        }

       
        public int InsertSnippet(Snippet snippet)
        {
           
            int count = 0;
            Connection();
            SqlCommand cmd = new SqlCommand("InsertSnippet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@title", snippet.Title);
            cmd.Parameters.AddWithValue("@snippetDescription", snippet.SnippetDescription);
            cmd.Parameters.AddWithValue("@content", snippet.Content);
            cmd.Parameters.AddWithValue("@email", snippet.Email );
            if (snippet.GroupName == null)
            {
                // Personal profile snippets don't have a group name
                cmd.Parameters.AddWithValue("@groupName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@groupName", snippet.GroupName);
            }

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public List<Snippet> ShowAllSnippets(string memberEmail)
        {
            List<Snippet> snippetList = new List<Snippet>();
            SqlDataReader reader;
            Connection();
            SqlCommand cmd = new SqlCommand("ShowAllSnippets", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Email";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = memberEmail;

            cmd.Parameters.Add(parameter);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Snippet snippet = new Snippet();
                    snippet.Title = reader[1].ToString();
                    snippet.SnippetDescription = reader[2].ToString();
                    snippet.Content = reader[3].ToString();
                    snippet.Email = reader[4].ToString();
                    snippetList.Add(snippet);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return snippetList;
        }

        public int InsertGroup(Group group)
        {
            int count = 0;
            Connection();
            SqlCommand cmd = new SqlCommand("CreateGroup", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupName", group.GroupName);
            cmd.Parameters.AddWithValue("@GroupDescription", group.GroupDescription);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public List<Group> FindGroups(string GroupName)
        {
            List<Group> GroupList = new List<Group>();
            SqlDataReader reader;
            Connection();
            SqlCommand cmd = new SqlCommand("ShowGroup", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@GroupName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = GroupName;

            cmd.Parameters.Add(parameter);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Group group = new Group();
                    group.GroupName = reader[0].ToString();
                    group.GroupDescription = reader[1].ToString();

                    GroupList.Add(group);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return GroupList;
        }

        public int InsertintoGroupMembers(Group group, string email)
        {

            int count = 0;
            Connection();
            SqlCommand cmd = new SqlCommand("Insert_Into_tbGroupMembers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GroupName", group.GroupName);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {

                conn.Close();
            }

            return count;
        }

        public List<Group> ShowMemberGroups(string memberEmail)
        {
            List<Group> groupList = new List<Group>();
            SqlDataReader reader;
            Connection();
            SqlCommand cmd = new SqlCommand("ShowMemberGroups", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Email";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = memberEmail;

            cmd.Parameters.Add(parameter);

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Group group = new Group();
                    group.GroupName = reader[0].ToString();
                    group.GroupDescription = reader[1].ToString();
                    groupList.Add(group);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return groupList;
        }

        public List<Snippet> ShowGroupSnippets(string groupName)
        {
            List<Snippet> snippetList = new List<Snippet>();
            SqlDataReader reader;
            Connection();
            SqlCommand cmd = new SqlCommand("ShowGroupSnippets", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@GroupName";
            parameter.SqlDbType = SqlDbType.VarChar;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = groupName;

          
            cmd.Parameters.Add(parameter);
            

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Snippet snippet = new Snippet();
                    snippet.Title = reader[1].ToString();
                    snippet.SnippetDescription = reader[2].ToString();
                    snippet.Content = reader[3].ToString();
                    snippet.Email = reader[4].ToString();
                    snippet.GroupName = reader[5].ToString();
                    snippetList.Add(snippet);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return snippetList;
        }


    }
}