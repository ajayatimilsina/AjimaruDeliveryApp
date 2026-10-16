namespace AjimaruDeliveryApp
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPageCustomer = new System.Windows.Forms.Button();
            this.btnPageKitchen = new System.Windows.Forms.Button();
            this.btnPageDelivery = new System.Windows.Forms.Button();
            this.pnlCustomer = new System.Windows.Forms.Panel();
            this.clbBulkItems = new System.Windows.Forms.CheckedListBox();
            this.btnAddBulkOrder = new System.Windows.Forms.Button();
            this.cmbOrderTypeFilter = new System.Windows.Forms.ComboBox();
            this.lblOrderTypeFilter = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCustomerEmail = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCustomerEmail = new System.Windows.Forms.TextBox();
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.btnAddOrder = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.cmbCustomerDelay = new System.Windows.Forms.ComboBox();
            this.btnCustomerSendMail = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.flpCustomerMenuCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKitchen = new System.Windows.Forms.Panel();
            this.cmbKitchenMenuType = new System.Windows.Forms.ComboBox();
            this.lblKitchenMenuType = new System.Windows.Forms.Label();
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.btnUploadPhoto = new System.Windows.Forms.Button();
            this.chkKitchenRecommended = new System.Windows.Forms.CheckBox();
            this.txtKitchenMenuPrice = new System.Windows.Forms.TextBox();
            this.txtKitchenMenuName = new System.Windows.Forms.TextBox();
            this.lblKitchenMenuPrice = new System.Windows.Forms.Label();
            this.lblKitchenMenuName = new System.Windows.Forms.Label();
            this.lblKitchenPhotoPath = new System.Windows.Forms.Label();
            this.dgvKitchenMenu = new System.Windows.Forms.DataGridView();
            this.flpKitchenMenuCards = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvKitchenOrders = new System.Windows.Forms.DataGridView();
            this.lblKitchen = new System.Windows.Forms.Label();
            this.pnlDelivery = new System.Windows.Forms.Panel();
            this.wbDeliveryMap = new System.Windows.Forms.WebBrowser();
            this.btnCallCustomer = new System.Windows.Forms.Button();
            this.btnOpenMap = new System.Windows.Forms.Button();
            this.cmbDeliveryDelay = new System.Windows.Forms.ComboBox();
            this.btnDeliverySendMail = new System.Windows.Forms.Button();
            this.lblDeliveryPrice = new System.Windows.Forms.Label();
            this.lblDeliveryPriceTitle = new System.Windows.Forms.Label();
            this.lblDeliveryAddress = new System.Windows.Forms.Label();
            this.lblDeliveryAddressTitle = new System.Windows.Forms.Label();
            this.dgvDeliveryOrders = new System.Windows.Forms.DataGridView();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.pnlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlKitchen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchenMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchenOrders)).BeginInit();
            this.pnlDelivery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPageCustomer
            // 
            this.btnPageCustomer.Location = new System.Drawing.Point(10, 10);
            this.btnPageCustomer.Name = "btnPageCustomer";
            this.btnPageCustomer.Size = new System.Drawing.Size(110, 32);
            this.btnPageCustomer.TabIndex = 0;
            this.btnPageCustomer.Text = "顧客";
            this.btnPageCustomer.UseVisualStyleBackColor = true;
            this.btnPageCustomer.Click += new System.EventHandler(this.btnPageCustomer_Click);
            // 
            // btnPageKitchen
            // 
            this.btnPageKitchen.Location = new System.Drawing.Point(130, 10);
            this.btnPageKitchen.Name = "btnPageKitchen";
            this.btnPageKitchen.Size = new System.Drawing.Size(110, 32);
            this.btnPageKitchen.TabIndex = 1;
            this.btnPageKitchen.Text = "キッチン";
            this.btnPageKitchen.UseVisualStyleBackColor = true;
            this.btnPageKitchen.Click += new System.EventHandler(this.btnPageKitchen_Click);
            // 
            // btnPageDelivery
            // 
            this.btnPageDelivery.Location = new System.Drawing.Point(250, 10);
            this.btnPageDelivery.Name = "btnPageDelivery";
            this.btnPageDelivery.Size = new System.Drawing.Size(110, 32);
            this.btnPageDelivery.TabIndex = 2;
            this.btnPageDelivery.Text = "配達";
            this.btnPageDelivery.UseVisualStyleBackColor = true;
            this.btnPageDelivery.Click += new System.EventHandler(this.btnPageDelivery_Click);
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.Controls.Add(this.clbBulkItems);
            this.pnlCustomer.Controls.Add(this.btnAddBulkOrder);
            this.pnlCustomer.Controls.Add(this.cmbOrderTypeFilter);
            this.pnlCustomer.Controls.Add(this.lblOrderTypeFilter);
            this.pnlCustomer.Controls.Add(this.lblCustomerName);
            this.pnlCustomer.Controls.Add(this.lblPhone);
            this.pnlCustomer.Controls.Add(this.lblAddress);
            this.pnlCustomer.Controls.Add(this.lblCustomerEmail);
            this.pnlCustomer.Controls.Add(this.txtCustomerName);
            this.pnlCustomer.Controls.Add(this.txtPhone);
            this.pnlCustomer.Controls.Add(this.txtAddress);
            this.pnlCustomer.Controls.Add(this.txtCustomerEmail);
            this.pnlCustomer.Controls.Add(this.cmbItem);
            this.pnlCustomer.Controls.Add(this.btnAddOrder);
            this.pnlCustomer.Controls.Add(this.btnComplete);
            this.pnlCustomer.Controls.Add(this.flpCustomerMenuCards);
            this.pnlCustomer.Controls.Add(this.dgvOrders);
            this.pnlCustomer.AutoScroll = true;
            this.pnlCustomer.Location = new System.Drawing.Point(10, 50);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(370, 700);
            this.pnlCustomer.TabIndex = 3;
            // 
            // clbBulkItems
            // 
            this.clbBulkItems.CheckOnClick = true;
            this.clbBulkItems.FormattingEnabled = true;
            this.clbBulkItems.Location = new System.Drawing.Point(10, 170);
            this.clbBulkItems.Name = "clbBulkItems";
            this.clbBulkItems.Size = new System.Drawing.Size(220, 61);
            this.clbBulkItems.TabIndex = 13;
            this.clbBulkItems.Visible = false;
            // 
            // btnAddBulkOrder
            // 
            this.btnAddBulkOrder.Location = new System.Drawing.Point(125, 145);
            this.btnAddBulkOrder.Name = "btnAddBulkOrder";
            this.btnAddBulkOrder.Size = new System.Drawing.Size(110, 32);
            this.btnAddBulkOrder.TabIndex = 12;
            this.btnAddBulkOrder.Text = "Bulk Add";
            this.btnAddBulkOrder.UseVisualStyleBackColor = true;
            // 
            // cmbOrderTypeFilter
            // 
            this.cmbOrderTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderTypeFilter.FormattingEnabled = true;
            this.cmbOrderTypeFilter.Location = new System.Drawing.Point(10, 135);
            this.cmbOrderTypeFilter.Name = "cmbOrderTypeFilter";
            this.cmbOrderTypeFilter.Size = new System.Drawing.Size(220, 23);
            this.cmbOrderTypeFilter.TabIndex = 11;
            // 
            // lblOrderTypeFilter
            // 
            this.lblOrderTypeFilter.AutoSize = true;
            this.lblOrderTypeFilter.Location = new System.Drawing.Point(10, 115);
            this.lblOrderTypeFilter.Name = "lblOrderTypeFilter";
            this.lblOrderTypeFilter.Size = new System.Drawing.Size(72, 15);
            this.lblOrderTypeFilter.TabIndex = 10;
            this.lblOrderTypeFilter.Text = "注文カテゴリ:";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(10, 22);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(43, 15);
            this.lblCustomerName.TabIndex = 15;
            this.lblCustomerName.Text = "名前";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(10, 57);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(67, 15);
            this.lblPhone.TabIndex = 16;
            this.lblPhone.Text = "電話番号";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(10, 92);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(31, 15);
            this.lblAddress.TabIndex = 17;
            this.lblAddress.Text = "住所";
            // 
            // lblCustomerEmail
            // 
            this.lblCustomerEmail.AutoSize = true;
            this.lblCustomerEmail.Location = new System.Drawing.Point(10, 127);
            this.lblCustomerEmail.Name = "lblCustomerEmail";
            this.lblCustomerEmail.Size = new System.Drawing.Size(76, 15);
            this.lblCustomerEmail.TabIndex = 18;
            this.lblCustomerEmail.Text = "メールアドレス";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(10, 40);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(340, 25);
            this.txtCustomerName.TabIndex = 0;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(10, 75);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(340, 25);
            this.txtPhone.TabIndex = 1;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(10, 110);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(340, 25);
            this.txtAddress.TabIndex = 2;
            // 
            // txtCustomerEmail
            // 
            this.txtCustomerEmail.Location = new System.Drawing.Point(10, 145);
            this.txtCustomerEmail.Name = "txtCustomerEmail";
            this.txtCustomerEmail.Size = new System.Drawing.Size(340, 25);
            this.txtCustomerEmail.TabIndex = 3;
            // 
            // cmbItem
            // 
            this.cmbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(10, 10);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(220, 23);
            this.cmbItem.TabIndex = 3;
            this.cmbItem.Visible = false;
            // 
            // btnAddOrder
            // 
            this.btnAddOrder.Location = new System.Drawing.Point(10, 180);
            this.btnAddOrder.Name = "btnAddOrder";
            this.btnAddOrder.Size = new System.Drawing.Size(110, 32);
            this.btnAddOrder.TabIndex = 4;
            this.btnAddOrder.Text = "カート追加";
            this.btnAddOrder.UseVisualStyleBackColor = true;
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(240, 180);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(110, 32);
            this.btnComplete.TabIndex = 5;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = true;
            // 
            // cmbCustomerDelay
            // 
            this.cmbCustomerDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerDelay.FormattingEnabled = true;
            this.cmbCustomerDelay.Location = new System.Drawing.Point(20, 122);
            this.cmbCustomerDelay.Name = "cmbCustomerDelay";
            this.cmbCustomerDelay.Size = new System.Drawing.Size(220, 23);
            this.cmbCustomerDelay.TabIndex = 19;
            // 
            // btnCustomerSendMail
            // 
            this.btnCustomerSendMail.Location = new System.Drawing.Point(246, 118);
            this.btnCustomerSendMail.Name = "btnCustomerSendMail";
            this.btnCustomerSendMail.Size = new System.Drawing.Size(104, 30);
            this.btnCustomerSendMail.TabIndex = 20;
            this.btnCustomerSendMail.Text = "Mail送信";
            this.btnCustomerSendMail.UseVisualStyleBackColor = true;
            // 
            // dgvOrders
            // 
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(10, 252);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.RowHeadersWidth = 51;
            this.dgvOrders.RowTemplate.Height = 24;
            this.dgvOrders.Size = new System.Drawing.Size(340, 130);
            this.dgvOrders.TabIndex = 6;
            // 
            // flpCustomerMenuCards
            // 
            this.flpCustomerMenuCards.AutoScroll = true;
            this.flpCustomerMenuCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpCustomerMenuCards.Location = new System.Drawing.Point(10, 398);
            this.flpCustomerMenuCards.Name = "flpCustomerMenuCards";
            this.flpCustomerMenuCards.Size = new System.Drawing.Size(340, 287);
            this.flpCustomerMenuCards.TabIndex = 14;
            this.flpCustomerMenuCards.WrapContents = false;
            // 
            // pnlKitchen
            // 
            this.pnlKitchen.Controls.Add(this.cmbKitchenMenuType);
            this.pnlKitchen.Controls.Add(this.lblKitchenMenuType);
            this.pnlKitchen.Controls.Add(this.btnAddMenu);
            this.pnlKitchen.Controls.Add(this.btnUploadPhoto);
            this.pnlKitchen.Controls.Add(this.chkKitchenRecommended);
            this.pnlKitchen.Controls.Add(this.txtKitchenMenuPrice);
            this.pnlKitchen.Controls.Add(this.txtKitchenMenuName);
            this.pnlKitchen.Controls.Add(this.lblKitchenMenuPrice);
            this.pnlKitchen.Controls.Add(this.lblKitchenMenuName);
            this.pnlKitchen.Controls.Add(this.cmbCustomerDelay);
            this.pnlKitchen.Controls.Add(this.btnCustomerSendMail);
            this.pnlKitchen.Controls.Add(this.lblKitchenPhotoPath);
            this.pnlKitchen.Controls.Add(this.flpKitchenMenuCards);
            this.pnlKitchen.Controls.Add(this.dgvKitchenMenu);
            this.pnlKitchen.Controls.Add(this.dgvKitchenOrders);
            this.pnlKitchen.Controls.Add(this.lblKitchen);
            this.pnlKitchen.AutoScroll = true;
            this.pnlKitchen.Location = new System.Drawing.Point(10, 50);
            this.pnlKitchen.Name = "pnlKitchen";
            this.pnlKitchen.Size = new System.Drawing.Size(370, 700);
            this.pnlKitchen.TabIndex = 4;
            this.pnlKitchen.Visible = false;
            // 
            // cmbKitchenMenuType
            // 
            this.cmbKitchenMenuType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbKitchenMenuType.FormattingEnabled = true;
            this.cmbKitchenMenuType.Location = new System.Drawing.Point(70, 85);
            this.cmbKitchenMenuType.Name = "cmbKitchenMenuType";
            this.cmbKitchenMenuType.Size = new System.Drawing.Size(100, 23);
            this.cmbKitchenMenuType.TabIndex = 12;
            this.cmbKitchenMenuType.Visible = false;
            // 
            // lblKitchenMenuType
            // 
            this.lblKitchenMenuType.AutoSize = true;
            this.lblKitchenMenuType.Location = new System.Drawing.Point(20, 89);
            this.lblKitchenMenuType.Name = "lblKitchenMenuType";
            this.lblKitchenMenuType.Size = new System.Drawing.Size(57, 15);
            this.lblKitchenMenuType.TabIndex = 11;
            this.lblKitchenMenuType.Text = "タイプ:";
            this.lblKitchenMenuType.Visible = false;
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Location = new System.Drawing.Point(255, 84);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(95, 28);
            this.btnAddMenu.TabIndex = 10;
            this.btnAddMenu.Text = "メニュー追加";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            // 
            // btnUploadPhoto
            // 
            this.btnUploadPhoto.Location = new System.Drawing.Point(180, 84);
            this.btnUploadPhoto.Name = "btnUploadPhoto";
            this.btnUploadPhoto.Size = new System.Drawing.Size(70, 28);
            this.btnUploadPhoto.TabIndex = 9;
            this.btnUploadPhoto.Text = "写真";
            this.btnUploadPhoto.UseVisualStyleBackColor = true;
            // 
            // chkKitchenRecommended
            // 
            this.chkKitchenRecommended.AutoSize = true;
            this.chkKitchenRecommended.Location = new System.Drawing.Point(262, 58);
            this.chkKitchenRecommended.Name = "chkKitchenRecommended";
            this.chkKitchenRecommended.Size = new System.Drawing.Size(74, 19);
            this.chkKitchenRecommended.TabIndex = 8;
            this.chkKitchenRecommended.Text = "おすすめ";
            this.chkKitchenRecommended.UseVisualStyleBackColor = true;
            // 
            // txtKitchenMenuPrice
            // 
            this.txtKitchenMenuPrice.Location = new System.Drawing.Point(207, 55);
            this.txtKitchenMenuPrice.Name = "txtKitchenMenuPrice";
            this.txtKitchenMenuPrice.Size = new System.Drawing.Size(49, 25);
            this.txtKitchenMenuPrice.TabIndex = 7;
            // 
            // txtKitchenMenuName
            // 
            this.txtKitchenMenuName.Location = new System.Drawing.Point(63, 55);
            this.txtKitchenMenuName.Name = "txtKitchenMenuName";
            this.txtKitchenMenuName.Size = new System.Drawing.Size(94, 25);
            this.txtKitchenMenuName.TabIndex = 6;
            // 
            // lblKitchenMenuPrice
            // 
            this.lblKitchenMenuPrice.AutoSize = true;
            this.lblKitchenMenuPrice.Location = new System.Drawing.Point(163, 59);
            this.lblKitchenMenuPrice.Name = "lblKitchenMenuPrice";
            this.lblKitchenMenuPrice.Size = new System.Drawing.Size(38, 15);
            this.lblKitchenMenuPrice.TabIndex = 5;
            this.lblKitchenMenuPrice.Text = "価格";
            // 
            // lblKitchenMenuName
            // 
            this.lblKitchenMenuName.AutoSize = true;
            this.lblKitchenMenuName.Location = new System.Drawing.Point(21, 59);
            this.lblKitchenMenuName.Name = "lblKitchenMenuName";
            this.lblKitchenMenuName.Size = new System.Drawing.Size(39, 15);
            this.lblKitchenMenuName.TabIndex = 4;
            this.lblKitchenMenuName.Text = "料理:";
            // 
            // lblKitchenPhotoPath
            // 
            this.lblKitchenPhotoPath.AutoSize = true;
            this.lblKitchenPhotoPath.Location = new System.Drawing.Point(20, 118);
            this.lblKitchenPhotoPath.Name = "lblKitchenPhotoPath";
            this.lblKitchenPhotoPath.Size = new System.Drawing.Size(84, 15);
            this.lblKitchenPhotoPath.TabIndex = 3;
            this.lblKitchenPhotoPath.Text = "写真: 未選択";
            // 
            // dgvKitchenMenu
            // 
            this.dgvKitchenMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKitchenMenu.Location = new System.Drawing.Point(20, 145);
            this.dgvKitchenMenu.Name = "dgvKitchenMenu";
            this.dgvKitchenMenu.RowHeadersWidth = 51;
            this.dgvKitchenMenu.RowTemplate.Height = 24;
            this.dgvKitchenMenu.Size = new System.Drawing.Size(330, 10);
            this.dgvKitchenMenu.TabIndex = 2;
            this.dgvKitchenMenu.Visible = false;
            // 
            // flpKitchenMenuCards
            // 
            this.flpKitchenMenuCards.AutoScroll = true;
            this.flpKitchenMenuCards.Location = new System.Drawing.Point(20, 145);
            this.flpKitchenMenuCards.Name = "flpKitchenMenuCards";
            this.flpKitchenMenuCards.Size = new System.Drawing.Size(330, 220);
            this.flpKitchenMenuCards.TabIndex = 13;
            // 
            // dgvKitchenOrders
            // 
            this.dgvKitchenOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKitchenOrders.Location = new System.Drawing.Point(20, 375);
            this.dgvKitchenOrders.Name = "dgvKitchenOrders";
            this.dgvKitchenOrders.RowHeadersWidth = 51;
            this.dgvKitchenOrders.RowTemplate.Height = 24;
            this.dgvKitchenOrders.Size = new System.Drawing.Size(330, 300);
            this.dgvKitchenOrders.TabIndex = 1;
            // 
            // lblKitchen
            // 
            this.lblKitchen.AutoSize = true;
            this.lblKitchen.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.lblKitchen.Location = new System.Drawing.Point(20, 20);
            this.lblKitchen.Name = "lblKitchen";
            this.lblKitchen.Size = new System.Drawing.Size(344, 24);
            this.lblKitchen.TabIndex = 0;
            this.lblKitchen.Text = "キッチン (メニュー追加 / 注文確認)";
            // 
            // pnlDelivery
            // 
            this.pnlDelivery.Controls.Add(this.wbDeliveryMap);
            this.pnlDelivery.Controls.Add(this.btnCallCustomer);
            this.pnlDelivery.Controls.Add(this.btnOpenMap);
            this.pnlDelivery.Controls.Add(this.cmbDeliveryDelay);
            this.pnlDelivery.Controls.Add(this.btnDeliverySendMail);
            this.pnlDelivery.Controls.Add(this.lblDeliveryPrice);
            this.pnlDelivery.Controls.Add(this.lblDeliveryPriceTitle);
            this.pnlDelivery.Controls.Add(this.lblDeliveryAddress);
            this.pnlDelivery.Controls.Add(this.lblDeliveryAddressTitle);
            this.pnlDelivery.Controls.Add(this.dgvDeliveryOrders);
            this.pnlDelivery.Controls.Add(this.lblDelivery);
            this.pnlDelivery.AutoScroll = true;
            this.pnlDelivery.Location = new System.Drawing.Point(10, 50);
            this.pnlDelivery.Name = "pnlDelivery";
            this.pnlDelivery.Size = new System.Drawing.Size(370, 700);
            this.pnlDelivery.TabIndex = 5;
            this.pnlDelivery.Visible = false;
            // 
            // wbDeliveryMap
            // 
            this.wbDeliveryMap.Location = new System.Drawing.Point(20, 184);
            this.wbDeliveryMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbDeliveryMap.Name = "wbDeliveryMap";
            this.wbDeliveryMap.ScriptErrorsSuppressed = true;
            this.wbDeliveryMap.Size = new System.Drawing.Size(330, 496);
            this.wbDeliveryMap.TabIndex = 7;
            // 
            // btnCallCustomer
            // 
            this.btnCallCustomer.Location = new System.Drawing.Point(20, 80);
            this.btnCallCustomer.Name = "btnCallCustomer";
            this.btnCallCustomer.Size = new System.Drawing.Size(330, 32);
            this.btnCallCustomer.TabIndex = 8;
            this.btnCallCustomer.Text = "Call Customer";
            this.btnCallCustomer.UseVisualStyleBackColor = true;
            // 
            // btnOpenMap
            // 
            this.btnOpenMap.Location = new System.Drawing.Point(20, 116);
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(330, 32);
            this.btnOpenMap.TabIndex = 6;
            this.btnOpenMap.Text = "Google Mapで住所を見る";
            this.btnOpenMap.UseVisualStyleBackColor = true;
            // 
            // cmbDeliveryDelay
            // 
            this.cmbDeliveryDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeliveryDelay.FormattingEnabled = true;
            this.cmbDeliveryDelay.Location = new System.Drawing.Point(20, 152);
            this.cmbDeliveryDelay.Name = "cmbDeliveryDelay";
            this.cmbDeliveryDelay.Size = new System.Drawing.Size(220, 23);
            this.cmbDeliveryDelay.TabIndex = 9;
            // 
            // btnDeliverySendMail
            // 
            this.btnDeliverySendMail.Location = new System.Drawing.Point(246, 149);
            this.btnDeliverySendMail.Name = "btnDeliverySendMail";
            this.btnDeliverySendMail.Size = new System.Drawing.Size(104, 28);
            this.btnDeliverySendMail.TabIndex = 10;
            this.btnDeliverySendMail.Text = "Mail送信";
            this.btnDeliverySendMail.UseVisualStyleBackColor = true;
            // 
            // lblDeliveryPrice
            // 
            this.lblDeliveryPrice.AutoSize = true;
            this.lblDeliveryPrice.Location = new System.Drawing.Point(84, 54);
            this.lblDeliveryPrice.Name = "lblDeliveryPrice";
            this.lblDeliveryPrice.Size = new System.Drawing.Size(12, 15);
            this.lblDeliveryPrice.TabIndex = 5;
            this.lblDeliveryPrice.Text = "-";
            // 
            // lblDeliveryPriceTitle
            // 
            this.lblDeliveryPriceTitle.AutoSize = true;
            this.lblDeliveryPriceTitle.Location = new System.Drawing.Point(20, 54);
            this.lblDeliveryPriceTitle.Name = "lblDeliveryPriceTitle";
            this.lblDeliveryPriceTitle.Size = new System.Drawing.Size(57, 15);
            this.lblDeliveryPriceTitle.TabIndex = 4;
            this.lblDeliveryPriceTitle.Text = "料金:";
            // 
            // lblDeliveryAddress
            // 
            this.lblDeliveryAddress.AutoSize = true;
            this.lblDeliveryAddress.Location = new System.Drawing.Point(84, 28);
            this.lblDeliveryAddress.Name = "lblDeliveryAddress";
            this.lblDeliveryAddress.Size = new System.Drawing.Size(12, 15);
            this.lblDeliveryAddress.TabIndex = 3;
            this.lblDeliveryAddress.Text = "-";
            // 
            // lblDeliveryAddressTitle
            // 
            this.lblDeliveryAddressTitle.AutoSize = true;
            this.lblDeliveryAddressTitle.Location = new System.Drawing.Point(20, 28);
            this.lblDeliveryAddressTitle.Name = "lblDeliveryAddressTitle";
            this.lblDeliveryAddressTitle.Size = new System.Drawing.Size(57, 15);
            this.lblDeliveryAddressTitle.TabIndex = 2;
            this.lblDeliveryAddressTitle.Text = "住所:";
            // 
            // dgvDeliveryOrders
            // 
            this.dgvDeliveryOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryOrders.Location = new System.Drawing.Point(20, 56);
            this.dgvDeliveryOrders.Name = "dgvDeliveryOrders";
            this.dgvDeliveryOrders.RowHeadersWidth = 51;
            this.dgvDeliveryOrders.RowTemplate.Height = 24;
            this.dgvDeliveryOrders.Size = new System.Drawing.Size(330, 10);
            this.dgvDeliveryOrders.TabIndex = 1;
            this.dgvDeliveryOrders.Visible = false;
            // 
            // lblDelivery
            // 
            this.lblDelivery.AutoSize = true;
            this.lblDelivery.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.lblDelivery.Location = new System.Drawing.Point(20, 20);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new System.Drawing.Size(268, 24);
            this.lblDelivery.TabIndex = 0;
            this.lblDelivery.Text = "配達 (住所 / 料金 / 地図)";
            this.lblDelivery.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 760);
            this.Controls.Add(this.pnlDelivery);
            this.Controls.Add(this.pnlKitchen);
            this.Controls.Add(this.pnlCustomer);
            this.Controls.Add(this.btnPageDelivery);
            this.Controls.Add(this.btnPageKitchen);
            this.Controls.Add(this.btnPageCustomer);
            this.Name = "Form1";
            this.Text = "Ajimaru Delivery App";
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlKitchen.ResumeLayout(false);
            this.pnlKitchen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchenMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKitchenOrders)).EndInit();
            this.pnlDelivery.ResumeLayout(false);
            this.pnlDelivery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPageCustomer;
        private System.Windows.Forms.Button btnPageKitchen;
        private System.Windows.Forms.Button btnPageDelivery;
        private System.Windows.Forms.Panel pnlCustomer;
        private System.Windows.Forms.Panel pnlKitchen;
        private System.Windows.Forms.Panel pnlDelivery;
        private System.Windows.Forms.CheckedListBox clbBulkItems;
        private System.Windows.Forms.Button btnAddBulkOrder;
        private System.Windows.Forms.ComboBox cmbOrderTypeFilter;
        private System.Windows.Forms.Label lblOrderTypeFilter;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCustomerEmail;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCustomerEmail;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Button btnAddOrder;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.ComboBox cmbCustomerDelay;
        private System.Windows.Forms.Button btnCustomerSendMail;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.FlowLayoutPanel flpCustomerMenuCards;
        private System.Windows.Forms.ComboBox cmbKitchenMenuType;
        private System.Windows.Forms.Label lblKitchenMenuType;
        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.Button btnUploadPhoto;
        private System.Windows.Forms.CheckBox chkKitchenRecommended;
        private System.Windows.Forms.TextBox txtKitchenMenuPrice;
        private System.Windows.Forms.TextBox txtKitchenMenuName;
        private System.Windows.Forms.Label lblKitchenMenuPrice;
        private System.Windows.Forms.Label lblKitchenMenuName;
        private System.Windows.Forms.Label lblKitchenPhotoPath;
        private System.Windows.Forms.DataGridView dgvKitchenMenu;
        private System.Windows.Forms.FlowLayoutPanel flpKitchenMenuCards;
        private System.Windows.Forms.DataGridView dgvKitchenOrders;
        private System.Windows.Forms.Label lblKitchen;
        private System.Windows.Forms.WebBrowser wbDeliveryMap;
        private System.Windows.Forms.Button btnCallCustomer;
        private System.Windows.Forms.Button btnOpenMap;
        private System.Windows.Forms.ComboBox cmbDeliveryDelay;
        private System.Windows.Forms.Button btnDeliverySendMail;
        private System.Windows.Forms.Label lblDeliveryPrice;
        private System.Windows.Forms.Label lblDeliveryPriceTitle;
        private System.Windows.Forms.Label lblDeliveryAddress;
        private System.Windows.Forms.Label lblDeliveryAddressTitle;
        private System.Windows.Forms.DataGridView dgvDeliveryOrders;
        private System.Windows.Forms.Label lblDelivery;
    }
}
