namespace FoodReco
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchButton = new System.Windows.Forms.Button();
            this.FoodItem1TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocationTextBox = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.MealTypeList = new System.Windows.Forms.CheckedListBox();
            this.VegNonVegList = new System.Windows.Forms.CheckedListBox();
            this.MealType = new System.Windows.Forms.Label();
            this.VegNonVeg = new System.Windows.Forms.Label();
            this.FoodItem5TextBox = new System.Windows.Forms.TextBox();
            this.FoodItem4TextBox = new System.Windows.Forms.TextBox();
            this.FoodItem3TextBox = new System.Windows.Forms.TextBox();
            this.FoodItem2TextBox = new System.Windows.Forms.TextBox();
            this.cuisineList = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.listBox4 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(176, 346);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Recommend";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FoodItem1TextBox
            // 
            this.FoodItem1TextBox.Location = new System.Drawing.Point(12, 25);
            this.FoodItem1TextBox.Name = "FoodItem1TextBox";
            this.FoodItem1TextBox.Size = new System.Drawing.Size(163, 20);
            this.FoodItem1TextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Food Items";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Location";
            // 
            // LocationTextBox
            // 
            this.LocationTextBox.Location = new System.Drawing.Point(12, 168);
            this.LocationTextBox.Name = "LocationTextBox";
            this.LocationTextBox.Size = new System.Drawing.Size(164, 20);
            this.LocationTextBox.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(431, 66);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(171, 251);
            this.listBox1.TabIndex = 5;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(466, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = "Recommendations (Using Average Score)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(845, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "You May Also Like";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(625, 66);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(187, 251);
            this.listBox2.TabIndex = 8;
            // 
            // MealTypeList
            // 
            this.MealTypeList.FormattingEnabled = true;
            this.MealTypeList.Items.AddRange(new object[] {
            "Breakfast",
            "Lunch",
            "Dinner"});
            this.MealTypeList.Location = new System.Drawing.Point(12, 245);
            this.MealTypeList.Name = "MealTypeList";
            this.MealTypeList.Size = new System.Drawing.Size(75, 49);
            this.MealTypeList.TabIndex = 9;
            // 
            // VegNonVegList
            // 
            this.VegNonVegList.FormattingEnabled = true;
            this.VegNonVegList.Items.AddRange(new object[] {
            "Vegetarian",
            "Non-Vegetarian"});
            this.VegNonVegList.Location = new System.Drawing.Point(93, 245);
            this.VegNonVegList.Name = "VegNonVegList";
            this.VegNonVegList.Size = new System.Drawing.Size(98, 34);
            this.VegNonVegList.TabIndex = 10;
            // 
            // MealType
            // 
            this.MealType.AutoSize = true;
            this.MealType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MealType.Location = new System.Drawing.Point(15, 222);
            this.MealType.Name = "MealType";
            this.MealType.Size = new System.Drawing.Size(66, 13);
            this.MealType.TabIndex = 11;
            this.MealType.Text = "Meal Type";
            // 
            // VegNonVeg
            // 
            this.VegNonVeg.AutoSize = true;
            this.VegNonVeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VegNonVeg.Location = new System.Drawing.Point(92, 222);
            this.VegNonVeg.Name = "VegNonVeg";
            this.VegNonVeg.Size = new System.Drawing.Size(92, 13);
            this.VegNonVeg.TabIndex = 12;
            this.VegNonVeg.Text = "Veg / Non Veg";
            // 
            // FoodItem5TextBox
            // 
            this.FoodItem5TextBox.Location = new System.Drawing.Point(12, 129);
            this.FoodItem5TextBox.Name = "FoodItem5TextBox";
            this.FoodItem5TextBox.Size = new System.Drawing.Size(163, 20);
            this.FoodItem5TextBox.TabIndex = 13;
            // 
            // FoodItem4TextBox
            // 
            this.FoodItem4TextBox.Location = new System.Drawing.Point(12, 103);
            this.FoodItem4TextBox.Name = "FoodItem4TextBox";
            this.FoodItem4TextBox.Size = new System.Drawing.Size(163, 20);
            this.FoodItem4TextBox.TabIndex = 14;
            // 
            // FoodItem3TextBox
            // 
            this.FoodItem3TextBox.Location = new System.Drawing.Point(12, 77);
            this.FoodItem3TextBox.Name = "FoodItem3TextBox";
            this.FoodItem3TextBox.Size = new System.Drawing.Size(163, 20);
            this.FoodItem3TextBox.TabIndex = 15;
            // 
            // FoodItem2TextBox
            // 
            this.FoodItem2TextBox.Location = new System.Drawing.Point(12, 51);
            this.FoodItem2TextBox.Name = "FoodItem2TextBox";
            this.FoodItem2TextBox.Size = new System.Drawing.Size(163, 20);
            this.FoodItem2TextBox.TabIndex = 16;
            // 
            // cuisineList
            // 
            this.cuisineList.FormattingEnabled = true;
            this.cuisineList.Items.AddRange(new object[] {
            "Mexican",
            "Chinese",
            "Italian",
            "Indian",
            "French",
            "Thai",
            "Continental"});
            this.cuisineList.Location = new System.Drawing.Point(12, 320);
            this.cuisineList.Name = "cuisineList";
            this.cuisineList.Size = new System.Drawing.Size(102, 109);
            this.cuisineList.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Cuisine";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FoodReco.Properties.Resources.chef11;
            this.pictureBox1.Location = new System.Drawing.Point(154, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 283);
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Colonna MT", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(172, 311);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 22);
            this.label6.TabIndex = 20;
            this.label6.Text = "लज़ीज़ व्यंजन सलाहकार";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(832, 66);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(187, 121);
            this.listBox3.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(651, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 39);
            this.label7.TabIndex = 6;
            this.label7.Text = "Recommendations (Using Nearest Neighbour)";
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(832, 222);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(187, 121);
            this.listBox4.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 432);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cuisineList);
            this.Controls.Add(this.FoodItem2TextBox);
            this.Controls.Add(this.FoodItem3TextBox);
            this.Controls.Add(this.FoodItem4TextBox);
            this.Controls.Add(this.FoodItem5TextBox);
            this.Controls.Add(this.VegNonVeg);
            this.Controls.Add(this.MealType);
            this.Controls.Add(this.VegNonVegList);
            this.Controls.Add(this.MealTypeList);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.LocationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FoodItem1TextBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Name = "Form1";
            this.Text = "लज़ीज़ व्यंजन सलाहकार";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox FoodItem1TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LocationTextBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.CheckedListBox MealTypeList;
        private System.Windows.Forms.CheckedListBox VegNonVegList;
        private System.Windows.Forms.Label MealType;
        private System.Windows.Forms.Label VegNonVeg;
        private System.Windows.Forms.TextBox FoodItem5TextBox;
        private System.Windows.Forms.TextBox FoodItem4TextBox;
        private System.Windows.Forms.TextBox FoodItem3TextBox;
        private System.Windows.Forms.TextBox FoodItem2TextBox;
        private System.Windows.Forms.CheckedListBox cuisineList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox listBox4;
    }
}

