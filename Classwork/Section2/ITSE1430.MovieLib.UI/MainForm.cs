﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITSE1430.MovieLib.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void exitToolStripMenuItem_Click( object sender, EventArgs e )
        {
            if (MessageBox.Show("Are you sure you want to exit?",
                    "Close", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            //aboutToolStripMenuItem.
            MessageBox.Show(this, "Sorry", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Basic Message Box syntax
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var form = new MovieForm(); // Calls the MovieForm

            if (form.ShowDialog(this) == DialogResult.Cancel)
                return;

            //MessageBox.Show("Adding  Movie");
            _database.Add(form.Movie); // Call the database to add the Movie data
            //Movie.Name = "";
            RefreshMovies(); // The MainForm Load is only loaded once when it is called. Have to make the data it will update/display available
        }

        private MovieDatabase _database = new MovieDatabase();

        //This method can be overridden in a derived type
        protected virtual void SomeFunction()
        { }

        //This method MUST be overridden in a derived type
        //protected abstract void SomeAbstractFunction();
       
       
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            _listMovies.DisplayMember = "Name"; // Setting the Display Member Property to Name property. (When it displays it looks for the name property)
            RefreshMovies();
        }

        

        private void RefreshMovies()
        {
            var movies = _database.GetAll();

            _listMovies.Items.Clear();
            _listMovies.Items.AddRange(movies); // Listbox property that displays the data stored in the Items property and adds an item to the list when we add
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            DeleteMovie();
        }

        private void DeleteMovie()
        {
            var item = GetSelectedMovie(); // Changing it to type Movie so we can have access to the name functionality
            if (item == null)
                return;

            _database.Remove(item.Name); // Access to name down here through member access
            RefreshMovies();
        }

        private void OnMovieEdit ( object sender, EventArgs e )
        {
            EditMovie();
        }

        private Movie GetSelectedMovie()
        {
            return _listMovies.SelectedItem as Movie; // Changing it to type Movie so we can have access to the name functionality   
        }

        private void OnMovieDoubleClick( object sender, EventArgs e )
        {
            EditMovie();
        }

        private void EditMovie()
        {
            //get selected movie, if any
            var item = GetSelectedMovie();
            if (item == null)
                return;

            // Show form with selected movie
            var form = new MovieForm();
            form.Movie = item;

            if (form.ShowDialog(this) == DialogResult.Cancel)
                return;

            _database.Edit(item.Name, form.Movie); // Call the database to add the Movie data

            RefreshMovies(); // The MainForm Load is only loaded once when it is called. Have to make the data it will update/display available
        }

        private void OnListKeyUp( object sender, KeyEventArgs e )
        {
            if (e.KeyData == Keys.Delete)
            {
                DeleteMovie();
            };
        }
    }
}
