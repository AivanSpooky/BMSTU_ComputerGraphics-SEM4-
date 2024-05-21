namespace _6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainG = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb_col_yellow = new System.Windows.Forms.RadioButton();
            this.rb_col_purple = new System.Windows.Forms.RadioButton();
            this.rb_col_red = new System.Windows.Forms.RadioButton();
            this.rb_col_green = new System.Windows.Forms.RadioButton();
            this.rb_col_blue = new System.Windows.Forms.RadioButton();
            this.rb_col_black = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_mode_delay = new System.Windows.Forms.RadioButton();
            this.rb_mode_nodelay = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_fill_time = new System.Windows.Forms.Label();
            this.btn_clean = new System.Windows.Forms.Button();
            this.btn_draw = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_close_fig = new System.Windows.Forms.Button();
            this.btn_add_point = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tb_y_entry = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tb_x_entry = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.tb_yz_entry = new System.Windows.Forms.TextBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.tb_xz_entry = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rb_input_segments = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainG)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainG
            // 
            this.mainG.BackColor = System.Drawing.Color.White;
            this.mainG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainG.Location = new System.Drawing.Point(12, 12);
            this.mainG.Name = "mainG";
            this.mainG.Size = new System.Drawing.Size(991, 739);
            this.mainG.TabIndex = 0;
            this.mainG.TabStop = false;
            this.mainG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainG_MouseDown);
            this.mainG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainG_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_col_yellow);
            this.groupBox1.Controls.Add(this.rb_col_purple);
            this.groupBox1.Controls.Add(this.rb_col_red);
            this.groupBox1.Controls.Add(this.rb_col_green);
            this.groupBox1.Controls.Add(this.rb_col_blue);
            this.groupBox1.Controls.Add(this.rb_col_black);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(1009, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 119);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Цвет закраски";
            // 
            // rb_col_yellow
            // 
            this.rb_col_yellow.AutoSize = true;
            this.rb_col_yellow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_yellow.Location = new System.Drawing.Point(131, 28);
            this.rb_col_yellow.Name = "rb_col_yellow";
            this.rb_col_yellow.Size = new System.Drawing.Size(88, 24);
            this.rb_col_yellow.TabIndex = 5;
            this.rb_col_yellow.Text = "Желтый";
            this.rb_col_yellow.UseVisualStyleBackColor = true;
            this.rb_col_yellow.CheckedChanged += new System.EventHandler(this.rb_col_yellow_CheckedChanged);
            // 
            // rb_col_purple
            // 
            this.rb_col_purple.AutoSize = true;
            this.rb_col_purple.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_purple.Location = new System.Drawing.Point(131, 58);
            this.rb_col_purple.Name = "rb_col_purple";
            this.rb_col_purple.Size = new System.Drawing.Size(126, 24);
            this.rb_col_purple.TabIndex = 4;
            this.rb_col_purple.Text = "Фиолетовый";
            this.rb_col_purple.UseVisualStyleBackColor = true;
            this.rb_col_purple.CheckedChanged += new System.EventHandler(this.rb_col_purple_CheckedChanged);
            // 
            // rb_col_red
            // 
            this.rb_col_red.AutoSize = true;
            this.rb_col_red.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_red.Location = new System.Drawing.Point(131, 88);
            this.rb_col_red.Name = "rb_col_red";
            this.rb_col_red.Size = new System.Drawing.Size(92, 24);
            this.rb_col_red.TabIndex = 3;
            this.rb_col_red.Text = "Красный";
            this.rb_col_red.UseVisualStyleBackColor = true;
            this.rb_col_red.CheckedChanged += new System.EventHandler(this.rb_col_red_CheckedChanged);
            // 
            // rb_col_green
            // 
            this.rb_col_green.AutoSize = true;
            this.rb_col_green.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_green.Location = new System.Drawing.Point(6, 88);
            this.rb_col_green.Name = "rb_col_green";
            this.rb_col_green.Size = new System.Drawing.Size(95, 24);
            this.rb_col_green.TabIndex = 2;
            this.rb_col_green.Text = "Зеленый";
            this.rb_col_green.UseVisualStyleBackColor = true;
            this.rb_col_green.CheckedChanged += new System.EventHandler(this.rb_col_green_CheckedChanged);
            // 
            // rb_col_blue
            // 
            this.rb_col_blue.AutoSize = true;
            this.rb_col_blue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_blue.Location = new System.Drawing.Point(6, 58);
            this.rb_col_blue.Name = "rb_col_blue";
            this.rb_col_blue.Size = new System.Drawing.Size(74, 24);
            this.rb_col_blue.TabIndex = 1;
            this.rb_col_blue.Text = "Синий";
            this.rb_col_blue.UseVisualStyleBackColor = true;
            this.rb_col_blue.CheckedChanged += new System.EventHandler(this.rb_col_blue_CheckedChanged);
            // 
            // rb_col_black
            // 
            this.rb_col_black.AutoSize = true;
            this.rb_col_black.Checked = true;
            this.rb_col_black.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_col_black.Location = new System.Drawing.Point(6, 28);
            this.rb_col_black.Name = "rb_col_black";
            this.rb_col_black.Size = new System.Drawing.Size(86, 24);
            this.rb_col_black.TabIndex = 0;
            this.rb_col_black.TabStop = true;
            this.rb_col_black.Text = "Черный";
            this.rb_col_black.UseVisualStyleBackColor = true;
            this.rb_col_black.CheckedChanged += new System.EventHandler(this.rb_col_black_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_mode_delay);
            this.groupBox2.Controls.Add(this.rb_mode_nodelay);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(1009, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режим закраски";
            // 
            // rb_mode_delay
            // 
            this.rb_mode_delay.AutoSize = true;
            this.rb_mode_delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_mode_delay.Location = new System.Drawing.Point(6, 58);
            this.rb_mode_delay.Name = "rb_mode_delay";
            this.rb_mode_delay.Size = new System.Drawing.Size(125, 24);
            this.rb_mode_delay.TabIndex = 6;
            this.rb_mode_delay.Text = "С задержкой";
            this.rb_mode_delay.UseVisualStyleBackColor = true;
            this.rb_mode_delay.CheckedChanged += new System.EventHandler(this.rb_mode_delay_CheckedChanged);
            // 
            // rb_mode_nodelay
            // 
            this.rb_mode_nodelay.AutoSize = true;
            this.rb_mode_nodelay.Checked = true;
            this.rb_mode_nodelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_mode_nodelay.Location = new System.Drawing.Point(6, 28);
            this.rb_mode_nodelay.Name = "rb_mode_nodelay";
            this.rb_mode_nodelay.Size = new System.Drawing.Size(133, 24);
            this.rb_mode_nodelay.TabIndex = 5;
            this.rb_mode_nodelay.TabStop = true;
            this.rb_mode_nodelay.Text = "Без задержки";
            this.rb_mode_nodelay.UseVisualStyleBackColor = true;
            this.rb_mode_nodelay.CheckedChanged += new System.EventHandler(this.rb_mode_nodelay_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_fill_time);
            this.groupBox3.Controls.Add(this.btn_clean);
            this.groupBox3.Controls.Add(this.btn_draw);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(1009, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 140);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Действия";
            // 
            // lbl_fill_time
            // 
            this.lbl_fill_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_fill_time.Location = new System.Drawing.Point(8, 94);
            this.lbl_fill_time.Name = "lbl_fill_time";
            this.lbl_fill_time.Size = new System.Drawing.Size(249, 43);
            this.lbl_fill_time.TabIndex = 6;
            this.lbl_fill_time.Text = "Время закраски последней области: ";
            // 
            // btn_clean
            // 
            this.btn_clean.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_clean.Location = new System.Drawing.Point(147, 28);
            this.btn_clean.Name = "btn_clean";
            this.btn_clean.Size = new System.Drawing.Size(110, 37);
            this.btn_clean.TabIndex = 5;
            this.btn_clean.Text = "Очистить";
            this.btn_clean.UseVisualStyleBackColor = true;
            this.btn_clean.Click += new System.EventHandler(this.btn_clean_Click);
            // 
            // btn_draw
            // 
            this.btn_draw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_draw.Location = new System.Drawing.Point(6, 28);
            this.btn_draw.Name = "btn_draw";
            this.btn_draw.Size = new System.Drawing.Size(110, 37);
            this.btn_draw.TabIndex = 4;
            this.btn_draw.Text = "Закрасить";
            this.btn_draw.UseVisualStyleBackColor = true;
            this.btn_draw.Click += new System.EventHandler(this.btn_draw_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(1009, 472);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(263, 186);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Точки";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_close_fig);
            this.groupBox5.Controls.Add(this.btn_add_point);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(6, 28);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(251, 152);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Добавить точку";
            // 
            // btn_close_fig
            // 
            this.btn_close_fig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_close_fig.Location = new System.Drawing.Point(131, 85);
            this.btn_close_fig.Name = "btn_close_fig";
            this.btn_close_fig.Size = new System.Drawing.Size(114, 61);
            this.btn_close_fig.TabIndex = 5;
            this.btn_close_fig.Text = "Замкнуть фигуру";
            this.btn_close_fig.UseVisualStyleBackColor = true;
            this.btn_close_fig.Click += new System.EventHandler(this.btn_close_fig_Click);
            // 
            // btn_add_point
            // 
            this.btn_add_point.Location = new System.Drawing.Point(6, 85);
            this.btn_add_point.Name = "btn_add_point";
            this.btn_add_point.Size = new System.Drawing.Size(120, 61);
            this.btn_add_point.TabIndex = 4;
            this.btn_add_point.Text = "Добавить точку";
            this.btn_add_point.UseVisualStyleBackColor = true;
            this.btn_add_point.Click += new System.EventHandler(this.btn_add_point_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tb_y_entry);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox7.Location = new System.Drawing.Point(131, 25);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(114, 54);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Y";
            // 
            // tb_y_entry
            // 
            this.tb_y_entry.Location = new System.Drawing.Point(6, 19);
            this.tb_y_entry.Name = "tb_y_entry";
            this.tb_y_entry.Size = new System.Drawing.Size(102, 22);
            this.tb_y_entry.TabIndex = 3;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tb_x_entry);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox6.Location = new System.Drawing.Point(6, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(119, 54);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Х";
            // 
            // tb_x_entry
            // 
            this.tb_x_entry.Location = new System.Drawing.Point(6, 19);
            this.tb_x_entry.Name = "tb_x_entry";
            this.tb_x_entry.Size = new System.Drawing.Size(107, 22);
            this.tb_x_entry.TabIndex = 3;
            this.tb_x_entry.TextChanged += new System.EventHandler(this.tb_x_entry_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.X,
            this.Y});
            this.dataGridView1.Location = new System.Drawing.Point(1009, 664);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 50;
            this.dataGridView1.Size = new System.Drawing.Size(263, 185);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // Index
            // 
            this.Index.DataPropertyName = "Index";
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 60;
            // 
            // X
            // 
            this.X.DataPropertyName = "X";
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 75;
            // 
            // Y
            // 
            this.Y.DataPropertyName = "Y";
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 75;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox8.Location = new System.Drawing.Point(12, 757);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(273, 92);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Затравочные пиксели";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 41);
            this.label2.TabIndex = 0;
            this.label2.Text = "Добавить затравочный пиксель: средняя кнопка мыши";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.col});
            this.dataGridView2.Location = new System.Drawing.Point(291, 767);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 50;
            this.dataGridView2.Size = new System.Drawing.Size(303, 82);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "X";
            this.dataGridViewTextBoxColumn2.HeaderText = "X";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 75;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Y";
            this.dataGridViewTextBoxColumn3.HeaderText = "Y";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 75;
            // 
            // col
            // 
            this.col.DataPropertyName = "col";
            this.col.HeaderText = "Color";
            this.col.Name = "col";
            this.col.ReadOnly = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.groupBox11);
            this.groupBox9.Controls.Add(this.groupBox12);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox9.Location = new System.Drawing.Point(612, 757);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(391, 92);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Добавить затравочный пиксель";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.tb_yz_entry);
            this.groupBox11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox11.Location = new System.Drawing.Point(131, 28);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(114, 54);
            this.groupBox11.TabIndex = 3;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Y";
            // 
            // tb_yz_entry
            // 
            this.tb_yz_entry.Location = new System.Drawing.Point(6, 19);
            this.tb_yz_entry.Name = "tb_yz_entry";
            this.tb_yz_entry.Size = new System.Drawing.Size(102, 22);
            this.tb_yz_entry.TabIndex = 3;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.tb_xz_entry);
            this.groupBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox12.Location = new System.Drawing.Point(6, 28);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(119, 54);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Х";
            // 
            // tb_xz_entry
            // 
            this.tb_xz_entry.Location = new System.Drawing.Point(6, 19);
            this.tb_xz_entry.Name = "tb_xz_entry";
            this.tb_xz_entry.Size = new System.Drawing.Size(107, 22);
            this.tb_xz_entry.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(863, 791);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 48);
            this.button2.TabIndex = 4;
            this.button2.Text = "Добавить затравочный пиксель";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.radioButton1);
            this.groupBox10.Controls.Add(this.rb_input_segments);
            this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox10.Location = new System.Drawing.Point(1009, 133);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(263, 72);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Режим ввода с мыши";
            // 
            // radioButton1
            // 
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(107, 25);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(112, 34);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Свободный";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_input_segments
            // 
            this.rb_input_segments.Checked = true;
            this.rb_input_segments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rb_input_segments.Location = new System.Drawing.Point(6, 25);
            this.rb_input_segments.Name = "rb_input_segments";
            this.rb_input_segments.Size = new System.Drawing.Size(95, 34);
            this.rb_input_segments.TabIndex = 0;
            this.rb_input_segments.TabStop = true;
            this.rb_input_segments.Text = "Отрезки";
            this.rb_input_segments.UseVisualStyleBackColor = true;
            this.rb_input_segments.CheckedChanged += new System.EventHandler(this.rb_input_segments_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 861);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mainG);
            this.Name = "Form1";
            this.Text = "Лабораторная работа №6";
            ((System.ComponentModel.ISupportInitialize)(this.mainG)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mainG;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb_col_purple;
        private System.Windows.Forms.RadioButton rb_col_red;
        private System.Windows.Forms.RadioButton rb_col_green;
        private System.Windows.Forms.RadioButton rb_col_blue;
        private System.Windows.Forms.RadioButton rb_col_black;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rb_mode_delay;
        private System.Windows.Forms.RadioButton rb_mode_nodelay;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_clean;
        private System.Windows.Forms.Button btn_draw;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox tb_y_entry;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tb_x_entry;
        private System.Windows.Forms.Button btn_add_point;
        private System.Windows.Forms.Button btn_close_fig;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.RadioButton rb_col_yellow;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lbl_fill_time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn col;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox tb_yz_entry;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.TextBox tb_xz_entry;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rb_input_segments;
    }
}

