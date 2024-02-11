﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ayubo
{
    public partial class Short : Form
    {
        public Short()
        {
            InitializeComponent();
        }
        string driver;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-TDDKJH3;Initial Catalog=Ayubo_leasure;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home obj = new Home();
            obj.Show();
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            try
            {
                string fullname = txtname.Text;
                string email = txtemail.Text;
                int telno = int.Parse(txttelno.Text);
                int nic = int.Parse(txtnic.Text);
                DateTime rent = dtprented.Value;
                int srading = int.Parse(txtreading.Text);
                string vtype = cmbvtype.Text;
                string vbrand = cmbvbrand.Text;

                if (cbwith.Checked == true)
                {
                    driver = "with driver";

                }
                if (cbwithout.Checked == true)
                {
                    driver = "without driver";
                }

                //sql command for insert button
                string insert_query = "INSERT INTO S_Tour VALUES ('" + fullname + "', '" + email + "', '" + rent + "', '" + telno + "', '" + nic + "', '" + vbrand + "', '" + vtype + "', '" + driver + "', '" + srading + "')";
                SqlCommand cmdInsert = new SqlCommand(insert_query, con);

                con.Open(); //open the sql connection
                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("Data saved successfully..!");

                //clear all text fields
                txtname.Text = "";
                txtemail.Text = "";
                txtnic.Text = "";
                txttelno.Text = "";
                cmbvbrand.Text = "";
                cmbvtype.Text = "";
                txtreading.Text = "";
                cbwith.Checked = false;
                cbwithout.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not saved suceesfully..! Try again..." + ex);
            }
            finally
            {
                con.Close();    //close sql connection
                BindData();
            }
            void BindData()
            {
                // show insert, update & delete functions through datagridview
                SqlCommand command = new SqlCommand("SELECT * FROM S_Tour", con);
                SqlDataAdapter Ayubo_Leisure = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                Ayubo_Leisure.Fill(dt);
                dgvshort.DataSource = dt;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //code for search button
                string searchnic = txtsearch.Text; //search text field

                // sql connection for search button
                string search_query = "SELECT * FROM S_Tour WHERE nic = '" + searchnic + "'";
                SqlCommand cmdSearch = new SqlCommand(search_query, con);

                con.Open(); // open the sql connection
                SqlDataReader r = cmdSearch.ExecuteReader();

                while (r.Read())
                {
                    //search all the text fields by index number
                    txtname.Text = r[0].ToString();
                    txtemail.Text = r[1].ToString();
                    txtnic.Text = r[4].ToString();
                    dtprented.Text = r[2].ToString();
                    //dtpreturned.Text = r[4].ToString();
                    txttelno.Text = r[3].ToString();
                    cmbvbrand.Text = r[5].ToString();
                    cmbvtype.Text = r[6].ToString();
                    string driver = r[7].ToString();
                    if (driver == "with driver")
                    {
                        cbwith.Checked = true;
                    }
                    if (driver == "without driver")
                    {
                        cbwithout.Checked = true;
                    }
                    txtreading.Text = r[8].ToString();


                    MessageBox.Show("Data showed successfully..!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching " + ex);
            }
            finally
            {
                con.Close();    //close the sql connection
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                //code for delete button
                string searchnic = txtsearch.Text;   //search text field

                //sql connection for delete button
                string delete_query = "DELETE S_Tour WHERE nic = '" + searchnic + "' ";
                SqlCommand cmdDelete = new SqlCommand(delete_query, con);

                con.Open(); //open sql connection
                cmdDelete.ExecuteNonQuery();
                MessageBox.Show("Data deleted successfully..!");

                txtsearch.Text = ""; //clear the text field
                txtname.Text = "";
                txtemail.Text = "";
                txtnic.Text = "";
                txttelno.Text = "";
                cmbvbrand.Text = "";
                cmbvtype.Text = "";
                txtreading.Text = "";
                cbwith.Checked = false;
                cbwithout.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not deleted suceesfully..! Try again..." + ex);
            }
            finally
            {
                con.Close();    //close the sql connection
                BindData();
            }
            void BindData()
            {
                // show insert, update & delete functions through datagridview
                SqlCommand command = new SqlCommand("SELECT * FROM S_Tour", con);
                SqlDataAdapter Ayubo_Leisure = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                Ayubo_Leisure.Fill(dt);
                dgvshort.DataSource = dt;
            }
        }
    }
}
