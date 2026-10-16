using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace AjimaruDeliveryApp
{
    public partial class Form1 : Form
    {
        public class MenuItemOption
        {
            public string Name { get; set; }
            public int PriceYen { get; set; }
            public string FoodType { get; set; }
            public bool IsRecommended { get; set; }
            public bool IsOnSale { get; set; }
            public string PhotoPath { get; set; }
            public int Stock { get; set; }

            public string DisplayName
            {
                get
                {
                    return Name + " - " + PriceYen + " yen" + (IsRecommended ? " ★おすすめ" : string.Empty);
                }
            }

            public string BulkDisplayName
            {
                get
                {
                    return Name + " - " + PriceYen + " yen";
                }
            }
        }

        // अर्डरको डाटा राख्नका लागि Class
        public class OrderItem
        {
            public int ID { get; set; }
            public string CustomerName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Item { get; set; }
            public string FoodType { get; set; }
            public string Status { get; set; }
            public int PriceYen { get; set; }
            public string Email { get; set; }
        }

        private BindingList<OrderItem> orderList = new BindingList<OrderItem>();
        private BindingList<MenuItemOption> menuList = new BindingList<MenuItemOption>();
        private int nextId = 1;
        private string selectedPhotoPath = string.Empty;
        private string selectedCustomerCategory = "すべて";
        private FlowLayoutPanel flpCustomerCategoryTabs;
        private Button btnKitchenPrintReceipt;
        private Button btnKitchenPrintSimpleReceipt;
        private PrintDocument receiptPrintDocument;
        private string receiptPrintTitle = "領収書";
        private string receiptPrintText = string.Empty;
        private string receiptPrintCustomerName = string.Empty;
        private string receiptPrintAddress = string.Empty;
        private string[] receiptPrintItemLines = new string[0];
        private int receiptPrintSubTotal = 0;
        private int receiptPrintDiscount = 0;
        private int receiptPrintTotal = 0;
        private const string StoreAddress = "Ajimaruya, 3 Chome-1-4 Higashinakajimacho, Nakagawa Ward, Nagoya, Aichi 454-0866";
        private const string KitchenPassword = "123456";

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            btnAddOrder.Click += btnAddOrder_Click;
            btnComplete.Click += btnComplete_Click;
            btnOpenMap.Click += btnOpenMap_Click;
            btnCallCustomer.Click += btnCallCustomer_Click;
            btnCustomerSendMail.Click += btnCustomerSendMail_Click;
            btnDeliverySendMail.Click += btnDeliverySendMail_Click;
            btnAddMenu.Click += btnAddMenu_Click;
            btnUploadPhoto.Click += btnUploadPhoto_Click;
            btnAddBulkOrder.Click += btnAddBulkOrder_Click;
            cmbOrderTypeFilter.SelectedIndexChanged += cmbOrderTypeFilter_SelectedIndexChanged;
            dgvOrders.CellDoubleClick += dgvOrders_CellDoubleClick;
            dgvKitchenMenu.CellContentClick += dgvKitchenMenu_CellContentClick;
            dgvKitchenMenu.CurrentCellDirtyStateChanged += dgvKitchenMenu_CurrentCellDirtyStateChanged;
            dgvDeliveryOrders.SelectionChanged += dgvDeliveryOrders_SelectionChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDefaultMenus();

            cmbKitchenMenuType.DropDownStyle = ComboBoxStyle.DropDown;
            cmbKitchenMenuType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbKitchenMenuType.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbKitchenMenuType.Items.Clear();
            cmbKitchenMenuType.Items.AddRange(new object[] { "弁当", "揚げ物", "ご飯", "麺", "その他" });
            cmbKitchenMenuType.SelectedIndex = 0;
            cmbKitchenMenuType.Visible = false;
            lblKitchenMenuType.Visible = false;
            lblKitchenMenuType.Text = string.Empty;

            cmbOrderTypeFilter.Items.Clear();
            cmbOrderTypeFilter.Items.AddRange(new object[] { "すべて", "弁当", "揚げ物", "ご飯", "麺", "その他" });
            cmbOrderTypeFilter.SelectedIndex = 0;
            cmbOrderTypeFilter.Visible = false;
            lblOrderTypeFilter.Visible = false;

            cmbCustomerDelay.Items.Clear();
            cmbCustomerDelay.Items.AddRange(new object[] { "10min", "20min", "30 min", "1時間", "InputTime" });
            cmbCustomerDelay.SelectedItem = "InputTime";

            cmbDeliveryDelay.Items.Clear();
            cmbDeliveryDelay.Items.AddRange(new object[] { "すみません", "10min", "20min", "30 min", "1時間", "InputTime" });
            cmbDeliveryDelay.SelectedItem = "InputTime";

            pnlCustomer.AutoScroll = true;
            pnlCustomer.AutoScrollMinSize = new Size(0, 760);
            pnlDelivery.AutoScrollMinSize = new Size(0, 700);
            clbBulkItems.HorizontalScrollbar = true;
            flpCustomerMenuCards.AutoScroll = true;
            flpCustomerMenuCards.Location = new Point(10, 398);
            flpCustomerMenuCards.Size = new Size(340, 287);
            InitializeCustomerCategoryTabs();

            lblCustomerEmail.Location = new Point(10, 127);
            txtCustomerEmail.Location = new Point(10, 145);
            txtCustomerEmail.Size = new Size(340, 25);

            btnAddOrder.Location = new Point(10, 180);
            btnAddBulkOrder.Location = new Point(125, 180);
            btnComplete.Location = new Point(240, 180);

            dgvOrders.Location = new Point(10, 220);
            dgvOrders.Size = new Size(340, 160);

            flpCustomerMenuCards.Location = new Point(10, 398);
            flpCustomerMenuCards.Size = new Size(340, 287);

            receiptPrintDocument = new PrintDocument();
            receiptPrintDocument.PrintPage += receiptPrintDocument_PrintPage;
            EnsureKitchenReceiptButton();

            dgvKitchenOrders.DataSource = orderList;
            dgvDeliveryOrders.DataSource = orderList;
            SetupKitchenMenuGrid();

            dgvKitchenMenu.Visible = true;
            flpKitchenMenuCards.Visible = false;
            ApplyDefaultMobileLayout();

            dgvDeliveryOrders.Visible = false;
            lblDelivery.Visible = false;
            lblDeliveryAddressTitle.Location = new Point(20, 28);
            lblDeliveryAddress.Location = new Point(84, 28);
            lblDeliveryPriceTitle.Location = new Point(20, 54);
            lblDeliveryPrice.Location = new Point(84, 54);
            btnCallCustomer.Location = new Point(20, 80);
            btnCallCustomer.Size = new Size(330, 32);
            btnOpenMap.Location = new Point(20, 116);
            btnOpenMap.Size = new Size(330, 32);
            cmbDeliveryDelay.Location = new Point(20, 152);
            cmbDeliveryDelay.Size = new Size(220, 23);
            btnDeliverySendMail.Location = new Point(246, 149);
            btnDeliverySendMail.Size = new Size(104, 28);
            cmbCustomerDelay.Location = new Point(20, 122);
            cmbCustomerDelay.Size = new Size(220, 23);
            btnCustomerSendMail.Location = new Point(246, 118);
            btnCustomerSendMail.Size = new Size(104, 30);
            wbDeliveryMap.Location = new Point(20, 184);
            wbDeliveryMap.Size = new Size(330, 496);

            RefreshCustomerOrdersByType();
            RefreshMenuItemsByType();
            ShowPage("Customer");
            UpdateDeliveryInfo();
            ApplyBootstrapTheme();
        }

        private void ApplyBootstrapTheme()
        {
            BackColor = Color.FromArgb(248, 249, 250);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            ApplyThemeToControl(this);
        }

        private void SetupKitchenMenuGrid()
        {
            dgvKitchenMenu.AutoGenerateColumns = false;
            dgvKitchenMenu.Columns.Clear();

            var colPhoto = new DataGridViewTextBoxColumn
            {
                Name = "colKitchenPhoto",
                HeaderText = "写真",
                DataPropertyName = nameof(MenuItemOption.PhotoPath),
                Width = 70,
                ReadOnly = true
            };

            var colName = new DataGridViewTextBoxColumn
            {
                Name = "colKitchenName",
                HeaderText = "商品名",
                DataPropertyName = nameof(MenuItemOption.Name),
                Width = 150
            };

            var colCategory = new DataGridViewTextBoxColumn
            {
                Name = "colKitchenCategory",
                HeaderText = "カテゴリ",
                DataPropertyName = nameof(MenuItemOption.FoodType),
                Width = 90
            };

            var colPrice = new DataGridViewTextBoxColumn
            {
                Name = "colKitchenPrice",
                HeaderText = "価格",
                DataPropertyName = nameof(MenuItemOption.PriceYen),
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "¥0" }
            };

            var colStock = new DataGridViewTextBoxColumn
            {
                Name = "colKitchenStock",
                HeaderText = "在庫",
                DataPropertyName = nameof(MenuItemOption.Stock),
                Width = 55
            };

            var colSale = new DataGridViewCheckBoxColumn
            {
                Name = "colKitchenSale",
                HeaderText = "販売",
                DataPropertyName = nameof(MenuItemOption.IsOnSale),
                Width = 60
            };

            var colEdit = new DataGridViewButtonColumn
            {
                Name = "colKitchenEdit",
                HeaderText = "オプション",
                Text = "編集",
                UseColumnTextForButtonValue = true,
                Width = 70
            };

            dgvKitchenMenu.Columns.AddRange(new DataGridViewColumn[]
            {
                colPhoto, colName, colCategory, colPrice, colStock, colSale, colEdit
            });

            dgvKitchenMenu.DataSource = menuList;
            dgvKitchenMenu.CellFormatting -= dgvKitchenMenu_CellFormatting;
            dgvKitchenMenu.CellFormatting += dgvKitchenMenu_CellFormatting;
        }

        private void ApplyThemeToControl(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = Color.FromArgb(13, 110, 253);
                    button.ForeColor = Color.White;
                }
                else if (control is TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.BackColor = Color.White;
                }
                else if (control is DataGridView grid)
                {
                    grid.BackgroundColor = Color.White;
                    grid.BorderStyle = BorderStyle.FixedSingle;
                    grid.GridColor = Color.FromArgb(222, 226, 230);
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(233, 236, 239);
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                }
                else if (control is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(248, 249, 250);
                }

                if (control.HasChildren)
                {
                    ApplyThemeToControl(control);
                }
            }
        }

        private void InitializeDefaultMenus()
        {
            menuList.Clear();
            menuList.Add(new MenuItemOption { Name = "唐揚げ (Karaage)", PriceYen = 500, FoodType = "揚げ物", IsRecommended = true, IsOnSale = true, PhotoPath = string.Empty, Stock = 20 });
            menuList.Add(new MenuItemOption { Name = "手羽先 (Tebasaki)", PriceYen = 600, FoodType = "揚げ物", IsRecommended = false, IsOnSale = true, PhotoPath = string.Empty, Stock = 15 });
            menuList.Add(new MenuItemOption { Name = "オムライス (Omurice)", PriceYen = 850, FoodType = "ご飯", IsRecommended = false, IsOnSale = true, PhotoPath = string.Empty, Stock = 8 });
        }

        // Add Order बटन थिच्दा
        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("कृपया ग्राहकको नाम र ठेगाना अनिवार्य राख्नुहोस्!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedMenu = cmbItem.SelectedItem as MenuItemOption;
            if (selectedMenu == null)
            {
                MessageBox.Show("メニューを選択してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!selectedMenu.IsOnSale)
            {
                MessageBox.Show("このメニューは販売停止中です。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedMenu.Stock <= 0)
            {
                MessageBox.Show("在庫がありません。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = selectedMenu.DisplayName;

            OrderItem newOrder = new OrderItem()
            {
                ID = nextId,
                CustomerName = txtCustomerName.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                Item = selectedItem,
                FoodType = selectedMenu.FoodType ?? "その他",
                Status = "Pending",
                PriceYen = selectedMenu.PriceYen,
                Email = txtCustomerEmail.Text
            };

            orderList.Add(newOrder);
            selectedMenu.Stock--;
            nextId++;
            RefreshCustomerOrdersByType();
            RenderMenuCards();

            // 同じ顧客の連続注文をしやすくするため、顧客情報は保持
            UpdateDeliveryInfo();
            cmbItem.Focus();
            MessageBox.Show("Delivery Order सफलतापूर्वक थपियो!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAddBulkOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("कृपया ग्राहकको नाम र ठेगाना अनिवार्य राख्नुहोस्!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedType = cmbOrderTypeFilter.SelectedItem?.ToString() ?? "すべて";
            if (selectedType != "すべて")
            {
                cmbOrderTypeFilter.SelectedItem = "すべて";
                MessageBox.Show("弁当と揚げ物をまとめて注文するには、カテゴリを『すべて』にしました。メニューをチェックしてから Bulk Add を押してください。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedMenus = clbBulkItems.CheckedItems.OfType<MenuItemOption>().ToList();
            if (selectedMenus.Count == 0)
            {
                MessageBox.Show("一括追加するメニューをチェックしてください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int addedCount = 0;
            int skippedCount = 0;
            foreach (var menu in selectedMenus)
            {
                if (!menu.IsOnSale || menu.Stock <= 0)
                {
                    skippedCount++;
                    continue;
                }

                orderList.Add(new OrderItem
                {
                    ID = nextId,
                    CustomerName = txtCustomerName.Text,
                    Phone = txtPhone.Text,
                    Address = txtAddress.Text,
                    Item = menu.DisplayName,
                    FoodType = menu.FoodType ?? "その他",
                    Status = "Pending",
                    PriceYen = menu.PriceYen,
                    Email = txtCustomerEmail.Text
                });

                menu.Stock--;
                nextId++;
                addedCount++;
            }

            RefreshCustomerOrdersByType();
            RenderMenuCards();
            UpdateDeliveryInfo();
            cmbItem.Focus();

            if (addedCount == 0)
            {
                MessageBox.Show("在庫があるメニューがありません。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var message = addedCount + "件の注文を一括追加しました。";
            if (skippedCount > 0)
            {
                message += " 在庫切れ " + skippedCount + " 件をスキップしました。";
            }

            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvKitchenMenu_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvKitchenMenu.IsCurrentCellDirty)
            {
                dgvKitchenMenu.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvKitchenMenu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (dgvKitchenMenu.Columns[e.ColumnIndex].Name == "colKitchenPhoto")
            {
                var row = dgvKitchenMenu.Rows[e.RowIndex].DataBoundItem as MenuItemOption;
                e.Value = row != null && !string.IsNullOrWhiteSpace(row.PhotoPath) ? "あり" : "-";
                e.FormattingApplied = true;
            }
        }

        private void dgvKitchenMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var menu = dgvKitchenMenu.Rows[e.RowIndex].DataBoundItem as MenuItemOption;
            if (menu == null)
            {
                return;
            }

            var columnName = dgvKitchenMenu.Columns[e.ColumnIndex].Name;
            if (columnName == "colKitchenSale")
            {
                var value = dgvKitchenMenu.Rows[e.RowIndex].Cells["colKitchenSale"].EditedFormattedValue;
                menu.IsOnSale = value is bool b && b;
                selectedCustomerCategory = "すべて";
                InitializeCustomerCategoryTabs();
                RefreshMenuItemsByType();
                return;
            }

            if (columnName == "colKitchenEdit")
            {
                if (ShowMenuEditDialog(menu))
                {
                    selectedCustomerCategory = "すべて";
                    InitializeCustomerCategoryTabs();
                    RefreshMenuItemsByType();
                    dgvKitchenMenu.Refresh();
                }
            }
        }

        private bool ShowMenuEditDialog(MenuItemOption menu)
        {
            using (var dialog = new Form())
            using (var lblName = new Label())
            using (var txtName = new TextBox())
            using (var lblType = new Label())
            using (var txtType = new TextBox())
            using (var lblPrice = new Label())
            using (var txtPrice = new TextBox())
            using (var lblStock = new Label())
            using (var txtStock = new TextBox())
            using (var lblPhoto = new Label())
            using (var lblPhotoValue = new Label())
            using (var btnSelectPhoto = new Button())
            using (var chkSale = new CheckBox())
            using (var btnOk = new Button())
            using (var btnCancel = new Button())
            {
                dialog.Text = "メニュー編集";
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.ClientSize = new Size(360, 300);

                var editedPhotoPath = menu.PhotoPath ?? string.Empty;

                lblName.Text = "商品名";
                lblName.Location = new Point(15, 15);
                lblName.AutoSize = true;
                txtName.Location = new Point(100, 12);
                txtName.Size = new Size(240, 25);
                txtName.Text = menu.Name;

                lblType.Text = "カテゴリ";
                lblType.Location = new Point(15, 50);
                lblType.AutoSize = true;
                txtType.Location = new Point(100, 47);
                txtType.Size = new Size(240, 25);
                txtType.Text = menu.FoodType;

                lblPrice.Text = "価格";
                lblPrice.Location = new Point(15, 85);
                lblPrice.AutoSize = true;
                txtPrice.Location = new Point(100, 82);
                txtPrice.Size = new Size(240, 25);
                txtPrice.Text = menu.PriceYen.ToString();

                lblStock.Text = "在庫";
                lblStock.Location = new Point(15, 120);
                lblStock.AutoSize = true;
                txtStock.Location = new Point(100, 117);
                txtStock.Size = new Size(240, 25);
                txtStock.Text = menu.Stock.ToString();

                lblPhoto.Text = "写真";
                lblPhoto.Location = new Point(15, 155);
                lblPhoto.AutoSize = true;

                lblPhotoValue.Location = new Point(100, 157);
                lblPhotoValue.Size = new Size(170, 15);
                lblPhotoValue.Text = string.IsNullOrWhiteSpace(editedPhotoPath) ? "未選択" : "選択済み";

                btnSelectPhoto.Text = "写真追加";
                btnSelectPhoto.Location = new Point(275, 150);
                btnSelectPhoto.Size = new Size(65, 28);
                btnSelectPhoto.Click += (s, e) =>
                {
                    using (var photoDialog = new OpenFileDialog())
                    {
                        photoDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                        photoDialog.Title = "写真を選択";
                        if (photoDialog.ShowDialog(dialog) == DialogResult.OK)
                        {
                            editedPhotoPath = photoDialog.FileName;
                            lblPhotoValue.Text = "選択済み";
                        }
                    }
                };

                chkSale.Text = "販売中";
                chkSale.Location = new Point(100, 185);
                chkSale.AutoSize = true;
                chkSale.Checked = menu.IsOnSale;

                btnOk.Text = "保存";
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Location = new Point(185, 245);
                btnOk.Size = new Size(75, 30);

                btnCancel.Text = "キャンセル";
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Location = new Point(265, 245);
                btnCancel.Size = new Size(75, 30);

                dialog.Controls.Add(lblName);
                dialog.Controls.Add(txtName);
                dialog.Controls.Add(lblType);
                dialog.Controls.Add(txtType);
                dialog.Controls.Add(lblPrice);
                dialog.Controls.Add(txtPrice);
                dialog.Controls.Add(lblStock);
                dialog.Controls.Add(txtStock);
                dialog.Controls.Add(lblPhoto);
                dialog.Controls.Add(lblPhotoValue);
                dialog.Controls.Add(btnSelectPhoto);
                dialog.Controls.Add(chkSale);
                dialog.Controls.Add(btnOk);
                dialog.Controls.Add(btnCancel);
                dialog.AcceptButton = btnOk;
                dialog.CancelButton = btnCancel;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtType.Text))
                {
                    MessageBox.Show("商品名とカテゴリを入力してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!int.TryParse(txtPrice.Text, out int price) || price <= 0)
                {
                    MessageBox.Show("価格は正しい数字で入力してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
                {
                    MessageBox.Show("在庫は0以上の数字で入力してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                menu.Name = txtName.Text.Trim();
                menu.FoodType = txtType.Text.Trim();
                menu.PriceYen = price;
                menu.Stock = stock;
                menu.PhotoPath = editedPhotoPath;
                menu.IsOnSale = chkSale.Checked;
                return true;
            }
        }

        private void RefreshMenuItemsByType()
        {
            var selectedType = cmbOrderTypeFilter.SelectedItem?.ToString() ?? "すべて";
            var filteredMenus = selectedType == "すべて"
                ? menuList.Where(m => m.IsOnSale).ToList()
                : menuList.Where(m => m.IsOnSale && m.FoodType == selectedType).ToList();

            var currentMenuName = (cmbItem.SelectedItem as MenuItemOption)?.Name;
            var checkedNames = clbBulkItems.CheckedItems.OfType<MenuItemOption>().Select(x => x.Name).ToList();

            cmbItem.DataSource = null;
            cmbItem.DataSource = filteredMenus;
            cmbItem.DisplayMember = "DisplayName";
            cmbItem.ValueMember = "Name";

            if (!string.IsNullOrWhiteSpace(currentMenuName))
            {
                var selectedIndex = filteredMenus.FindIndex(x => x.Name == currentMenuName);
                if (selectedIndex >= 0)
                {
                    cmbItem.SelectedIndex = selectedIndex;
                }
                else if (filteredMenus.Count > 0)
                {
                    cmbItem.SelectedIndex = 0;
                }
            }
            else if (filteredMenus.Count > 0)
            {
                cmbItem.SelectedIndex = 0;
            }

            clbBulkItems.DisplayMember = "BulkDisplayName";
            clbBulkItems.Items.Clear();
            foreach (var menu in filteredMenus)
            {
                int index = clbBulkItems.Items.Add(menu);
                if (checkedNames.Contains(menu.Name))
                {
                    clbBulkItems.SetItemChecked(index, true);
                }
            }

            RenderMenuCards();
        }

        private void RenderMenuCards()
        {
            var customerMenus = selectedCustomerCategory == "すべて"
                ? menuList.Where(m => m.IsOnSale).ToList()
                : menuList.Where(m => m.IsOnSale && IsMenuInCustomerCategory(m, selectedCustomerCategory)).ToList();

            RenderMenuCardList(flpCustomerMenuCards, customerMenus);
            RenderMenuCardList(flpKitchenMenuCards, menuList.ToList());
        }

        private void RenderMenuCardList(FlowLayoutPanel panel, System.Collections.Generic.List<MenuItemOption> menus)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            foreach (var menu in menus)
            {
                panel.Controls.Add(CreateMenuCard(menu));
            }

            panel.ResumeLayout();
        }

        private Control CreateMenuCard(MenuItemOption menu)
        {
            var card = new Panel
            {
                Width = 320,
                Height = 96,
                Margin = new Padding(4),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            ApplyRoundedRegion(card, 18);
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(224, 224, 224), 1))
                using (var path = CreateRoundedRectanglePath(new Rectangle(0, 0, card.Width - 1, card.Height - 1), 18))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            };

            var picture = new PictureBox
            {
                Location = new Point(6, 6),
                Size = new Size(92, 84),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(233, 236, 239)
            };

            if (!string.IsNullOrWhiteSpace(menu.PhotoPath) && File.Exists(menu.PhotoPath))
            {
                try
                {
                    picture.Image = Image.FromFile(menu.PhotoPath);
                }
                catch
                {
                    picture.Image = null;
                }
            }

            var lblName = new Label
            {
                Location = new Point(106, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = menu.Name
            };

            var lblType = new Label
            {
                Location = new Point(106, 36),
                AutoSize = true,
                Text = menu.FoodType
            };

            var lblPrice = new Label
            {
                Location = new Point(106, 61),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Text = "¥" + menu.PriceYen
            };

            var lblStock = new Label
            {
                Location = new Point(168, 64),
                AutoSize = true,
                ForeColor = menu.Stock <= 5 ? Color.FromArgb(220, 53, 69) : Color.FromArgb(25, 135, 84),
                Text = menu.Stock <= 5 ? "残りわずか" : "在庫 " + menu.Stock
            };

            var btnPlus = new Button
            {
                Location = new Point(280, 32),
                Size = new Size(32, 32),
                Text = "+",
                Tag = menu,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(226, 240, 232),
                ForeColor = Color.FromArgb(33, 106, 66),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnPlus.FlatAppearance.BorderSize = 0;
            ApplyRoundedRegion(btnPlus, 14);

            btnPlus.Click += (s, e) => AddSingleOrderFromMenu(menu);

            EventHandler cardClickHandler = (s, e) => AddSingleOrderFromMenu(menu);
            card.Click += cardClickHandler;
            picture.Click += cardClickHandler;
            lblName.Click += cardClickHandler;
            lblType.Click += cardClickHandler;
            lblPrice.Click += cardClickHandler;
            lblStock.Click += cardClickHandler;

            card.Cursor = Cursors.Hand;
            picture.Cursor = Cursors.Hand;
            lblName.Cursor = Cursors.Hand;
            lblType.Cursor = Cursors.Hand;
            lblPrice.Cursor = Cursors.Hand;
            lblStock.Cursor = Cursors.Hand;

            card.Controls.Add(picture);
            card.Controls.Add(lblName);
            card.Controls.Add(lblType);
            card.Controls.Add(lblPrice);
            card.Controls.Add(lblStock);
            card.Controls.Add(btnPlus);

            return card;
        }

        private void InitializeCustomerCategoryTabs()
        {
            if (flpCustomerCategoryTabs == null)
            {
                flpCustomerCategoryTabs = new FlowLayoutPanel
                {
                    Name = "flpCustomerCategoryTabs",
                    Location = new Point(10, 361),
                    Size = new Size(340, 36),
                    AutoScroll = true,
                    WrapContents = false,
                    FlowDirection = FlowDirection.LeftToRight,
                    BackColor = Color.Transparent,
                    Padding = new Padding(0, 2, 0, 2)
                };

                pnlCustomer.Controls.Add(flpCustomerCategoryTabs);
            }

            var categories = new System.Collections.Generic.List<string> { "すべて" };
            categories.AddRange(menuList
                .Select(m => m.FoodType)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .OrderBy(x => x));

            if (!categories.Contains(selectedCustomerCategory))
            {
                selectedCustomerCategory = "すべて";
            }

            flpCustomerCategoryTabs.SuspendLayout();
            flpCustomerCategoryTabs.Controls.Clear();

            for (int i = 0; i < categories.Count; i++)
            {
                var category = categories[i];
                var button = new Button
                {
                    Width = 82,
                    Height = 30,
                    Text = category,
                    Tag = category,
                    FlatStyle = FlatStyle.Flat,
                    Margin = i == categories.Count - 1 ? new Padding(0) : new Padding(0, 0, 4, 0)
                };

                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 230);
                ApplyRoundedRegion(button, 14);
                button.Click += (s, e) =>
                {
                    selectedCustomerCategory = (string)((Button)s).Tag;
                    UpdateCustomerCategoryTabStyles();
                    RenderMenuCards();
                };

                flpCustomerCategoryTabs.Controls.Add(button);
            }

            flpCustomerCategoryTabs.ResumeLayout();
            flpCustomerCategoryTabs.BringToFront();
            UpdateCustomerCategoryTabStyles();
        }

        private void UpdateCustomerCategoryTabStyles()
        {
            if (flpCustomerCategoryTabs == null)
            {
                return;
            }

            foreach (Control control in flpCustomerCategoryTabs.Controls)
            {
                var button = control as Button;
                if (button == null)
                {
                    continue;
                }

                var isSelected = string.Equals(button.Tag as string, selectedCustomerCategory, StringComparison.Ordinal);
                if (isSelected)
                {
                    button.BackColor = Color.FromArgb(25, 96, 66);
                    button.ForeColor = Color.White;
                    button.FlatAppearance.BorderColor = Color.FromArgb(25, 96, 66);
                }
                else
                {
                    button.BackColor = Color.White;
                    button.ForeColor = Color.FromArgb(73, 80, 87);
                    button.FlatAppearance.BorderColor = Color.FromArgb(222, 226, 230);
                }
            }
        }

        private bool IsMenuInCustomerCategory(MenuItemOption menu, string category)
        {
            if (string.Equals(category, "すべて", StringComparison.Ordinal))
            {
                return true;
            }

            return string.Equals(menu.FoodType, category, StringComparison.Ordinal);
        }

        private void ApplyRoundedRegion(Control control, int radius)
        {
            using (var path = CreateRoundedRectanglePath(new Rectangle(0, 0, control.Width, control.Height), radius))
            {
                control.Region = new Region(path);
            }

            control.Resize += (s, e) =>
            {
                using (var resizedPath = CreateRoundedRectanglePath(new Rectangle(0, 0, control.Width, control.Height), radius))
                {
                    control.Region = new Region(resizedPath);
                }
            };
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void AddSingleOrderFromMenu(MenuItemOption menu)
        {
            var customerName = string.IsNullOrWhiteSpace(txtCustomerName.Text) ? "Guest" : txtCustomerName.Text.Trim();
            var address = string.IsNullOrWhiteSpace(txtAddress.Text) ? "未入力" : txtAddress.Text.Trim();

            if (!menu.IsOnSale)
            {
                MessageBox.Show("このメニューは販売停止中です。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (menu.Stock <= 0)
            {
                MessageBox.Show("在庫がありません。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            orderList.Add(new OrderItem
            {
                ID = nextId,
                CustomerName = customerName,
                Phone = txtPhone.Text,
                Address = address,
                Item = menu.DisplayName,
                FoodType = menu.FoodType ?? "その他",
                Status = "Pending",
                PriceYen = menu.PriceYen,
                Email = txtCustomerEmail.Text
            });

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                txtCustomerName.Text = customerName;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                txtAddress.Text = address;
            }

            menu.Stock--;
            nextId++;
            RefreshCustomerOrdersByType();
            RenderMenuCards();
            UpdateDeliveryInfo();
            MessageBox.Show("注文を追加しました。", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            var menuName = txtKitchenMenuName.Text.Trim();
            if (string.IsNullOrWhiteSpace(menuName))
            {
                MessageBox.Show("メニュー名を入力してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtKitchenMenuPrice.Text.Trim(), out int price) || price <= 0)
            {
                MessageBox.Show("価格は正しい数字で入力してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedType = cmbKitchenMenuType.Text?.Trim();
            if (string.IsNullOrWhiteSpace(selectedType))
            {
                MessageBox.Show("料理タイプを入力または選択してください。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!cmbKitchenMenuType.Items.Contains(selectedType))
            {
                cmbKitchenMenuType.Items.Add(selectedType);
            }

            var newMenu = new MenuItemOption
            {
                Name = menuName,
                PriceYen = price,
                FoodType = selectedType,
                IsRecommended = chkKitchenRecommended.Checked,
                IsOnSale = true,
                PhotoPath = selectedPhotoPath,
                Stock = 100
            };

            menuList.Add(newMenu);
            selectedCustomerCategory = "すべて";
            InitializeCustomerCategoryTabs();
            RefreshMenuItemsByType();

            txtKitchenMenuName.Clear();
            txtKitchenMenuPrice.Clear();
            chkKitchenRecommended.Checked = false;
            cmbKitchenMenuType.Text = selectedType;
            selectedPhotoPath = string.Empty;
            lblKitchenPhotoPath.Text = "写真: 未選択";

            MessageBox.Show("メニューを追加しました。", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dialog.Title = "写真を選択";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPhotoPath = dialog.FileName;
                    lblKitchenPhotoPath.Text = "写真: 選択済み";
                }
            }
        }

        private void btnKitchenPrintReceipt_Click(object sender, EventArgs e)
        {
            PrintKitchenReceipt("領収書");
        }

        private void btnKitchenPrintSimpleReceipt_Click(object sender, EventArgs e)
        {
            PrintKitchenReceipt("リシート");
        }

        private void PrintKitchenReceipt(string title)
        {
            if (!(dgvKitchenOrders.CurrentRow?.DataBoundItem is OrderItem selectedOrder) || string.IsNullOrWhiteSpace(selectedOrder.CustomerName))
            {
                MessageBox.Show(title + "を印刷する注文を選択してください。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customerName = selectedOrder.CustomerName.Trim();
            var sameNameOrders = orderList
                .Where(o => string.Equals((o.CustomerName ?? string.Empty).Trim(), customerName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (sameNameOrders.Count == 0)
            {
                MessageBox.Show("同じ名前の注文が見つかりません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ShowReceiptPreview(title, customerName, "同名まとめ", sameNameOrders);
        }

        private void ShowReceiptPreview(string title, string customerName, string targetText, System.Collections.Generic.List<OrderItem> orders)
        {
            var total = orders.Sum(o => o.PriceYen);
            var lines = orders
                .Select(o => string.Format("{0} × 1  ¥{1}", o.Item, o.PriceYen))
                .ToArray();

            var address = orders
                .Select(o => o.Address)
                .FirstOrDefault(a => !string.IsNullOrWhiteSpace(a));

            receiptPrintTitle = string.IsNullOrWhiteSpace(title) ? "領収書" : title;
            receiptPrintCustomerName = customerName ?? string.Empty;
            receiptPrintAddress = string.IsNullOrWhiteSpace(address) ? "-" : address;
            receiptPrintItemLines = lines;
            receiptPrintSubTotal = total;
            receiptPrintDiscount = 0;
            receiptPrintTotal = receiptPrintSubTotal - receiptPrintDiscount;

            receiptPrintText =
                "日付: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "\n" +
                "宛名: " + customerName + " 様\n" +
                "住所: " + receiptPrintAddress + "\n\n" +
                "【ご注文内容】\n" +
                string.Join("\n", receiptPrintItemLines.Select(x => "- " + x)) + "\n\n" +
                "合計: ¥" + total + "\n\n" +
                "Ajimaru Delivery App";

            using (var preview = new PrintPreviewDialog())
            {
                preview.Document = receiptPrintDocument;
                preview.Width = receiptPrintTitle == "領収書" ? 420 : 900;
                preview.Height = receiptPrintTitle == "領収書" ? 760 : 700;
                preview.ShowDialog(this);
            }
        }

        private void receiptPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (receiptPrintTitle == "領収書")
            {
                using (var nameFont = new Font("MS UI Gothic", 15, FontStyle.Bold))
                using (var titleFont = new Font("MS UI Gothic", 17, FontStyle.Bold))
                using (var bodyFont = new Font("MS UI Gothic", 12, FontStyle.Regular))
                using (var totalFont = new Font("MS UI Gothic", 18, FontStyle.Bold))
                {
                    float left = 30;
                    float right = e.MarginBounds.Right - 30;
                    float y = 24;

                    e.Graphics.DrawString(receiptPrintCustomerName + " 様", nameFont, Brushes.Black, left + 85, y);
                    y += 40;
                    e.Graphics.DrawString("領収書", titleFont, Brushes.Black, left + 120, y);
                    y += 48;

                    foreach (var line in receiptPrintItemLines)
                    {
                        e.Graphics.DrawString(line, bodyFont, Brushes.Black, left, y);
                        y += 32;
                    }

                    y += 8;
                    e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
                    y += 20;
                    e.Graphics.DrawString("小計", bodyFont, Brushes.Black, left, y);
                    e.Graphics.DrawString("¥" + receiptPrintSubTotal, bodyFont, Brushes.Black, right - 90, y);

                    y += 30;
                    e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
                    y += 16;
                    e.Graphics.DrawString("割引", bodyFont, Brushes.Black, left, y);
                    e.Graphics.DrawString("- ¥" + receiptPrintDiscount, bodyFont, Brushes.Black, right - 90, y);

                    y += 30;
                    e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
                    y += 24;
                    e.Graphics.DrawString("合計", totalFont, Brushes.Black, left, y);
                    e.Graphics.DrawString("¥" + receiptPrintTotal, totalFont, Brushes.Black, right - 105, y);

                    y += 60;
                    e.Graphics.DrawString("但し お飲食代として", bodyFont, Brushes.Black, left + 60, y);
                    y += 30;
                    e.Graphics.DrawString("上記正に領収いたしました", bodyFont, Brushes.Black, left + 50, y);
                }

                e.HasMorePages = false;
                return;
            }

            using (var titleFont = new Font("MS UI Gothic", 16, FontStyle.Bold))
            using (var bodyFont = new Font("MS UI Gothic", 11, FontStyle.Regular))
            {
                float left = 40;
                float top = 40;
                e.Graphics.DrawString(receiptPrintTitle, titleFont, Brushes.Black, left, top);
                e.Graphics.DrawString(receiptPrintText, bodyFont, Brushes.Black, left, top + 35);
            }

            e.HasMorePages = false;
        }

        private void cmbOrderTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMenuItemsByType();
        }

        private void RefreshCustomerOrdersByType()
        {
            // 注文一覧は常に全件表示（カテゴリは注文選択用）
            dgvOrders.DataSource = orderList;
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (!(dgvOrders.Rows[e.RowIndex].DataBoundItem is OrderItem selectedOrder))
            {
                return;
            }

            if (selectedOrder.Status == "Delivered")
            {
                MessageBox.Show("Complete後の注文は削除できません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("この注文を削除しますか？", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            var menu = menuList.FirstOrDefault(m => m.DisplayName == selectedOrder.Item);
            if (menu != null)
            {
                menu.Stock++;
            }

            orderList.Remove(selectedOrder);
            dgvOrders.Refresh();
            dgvKitchenOrders.Refresh();
            dgvDeliveryOrders.Refresh();
            RenderMenuCards();
            UpdateDeliveryInfo();
        }

        // Complete बटन थिच्दा
        private void btnComplete_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is OrderItem selectedOrder)
            {
                selectedOrder.Status = "Delivered";

                dgvOrders.Refresh();
                dgvKitchenOrders.Refresh();
                dgvDeliveryOrders.Refresh();
                UpdateDeliveryInfo();
                MessageBox.Show("अर्डर 'Delivered' (पुर्‍याइयो) マークしました!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("कृपया सूचीबाट अर्डर छान्नुहोस्。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPageCustomer_Click(object sender, EventArgs e)
        {
            ShowPage("Customer");
        }

        private void btnPageKitchen_Click(object sender, EventArgs e)
        {
            if (ShowKitchenPasswordDialog())
            {
                ShowPage("Kitchen");
            }
            else
            {
                MessageBox.Show("パスワードが違います。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPageDelivery_Click(object sender, EventArgs e)
        {
            ShowPage("Delivery");
            UpdateDeliveryInfo();
        }

        private void dgvDeliveryOrders_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDeliveryInfo();
        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedDeliveryOrder();
            if (selectedOrder == null || string.IsNullOrWhiteSpace(selectedOrder.Address))
            {
                MessageBox.Show("住所が選択されていません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var mapUrl = "https://www.google.com/maps/dir/?api=1&origin="
                + Uri.EscapeDataString(StoreAddress)
                + "&destination=" + Uri.EscapeDataString(selectedOrder.Address)
                + "&travelmode=driving";
            try
            {
                Process.Start(mapUrl);
            }
            catch
            {
                MessageBox.Show("Google Map を開けませんでした。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCallCustomer_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedDeliveryOrder();
            if (selectedOrder == null || string.IsNullOrWhiteSpace(selectedOrder.Phone))
            {
                MessageBox.Show("電話番号がありません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Process.Start("tel:" + selectedOrder.Phone.Trim());
            }
            catch
            {
                MessageBox.Show("電話アプリを開けませんでした。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCustomerSendMail_Click(object sender, EventArgs e)
        {
            if (!(dgvKitchenOrders.CurrentRow?.DataBoundItem is OrderItem selectedOrder) || string.IsNullOrWhiteSpace(selectedOrder.Email))
            {
                MessageBox.Show("注文のメールアドレスがありません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var subject = "配達時間のご連絡";
            var body = BuildDeliveryMailBody(cmbCustomerDelay.SelectedItem?.ToString(), false);
            OpenMailClient(selectedOrder.Email.Trim(), subject, body);
        }

        private void btnDeliverySendMail_Click(object sender, EventArgs e)
        {
            var selectedOrder = GetSelectedDeliveryOrder();
            if (selectedOrder == null || string.IsNullOrWhiteSpace(selectedOrder.Email))
            {
                MessageBox.Show("注文のメールアドレスがありません。", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var subject = "配達時間のご連絡";
            var selectedDelay = cmbDeliveryDelay.SelectedItem?.ToString();
            var body = BuildDeliveryMailBody(selectedDelay, string.Equals(selectedDelay, "すみません", StringComparison.Ordinal));
            OpenMailClient(selectedOrder.Email.Trim(), subject, body);
        }

        private string BuildDeliveryMailBody(string selectedDelay, bool withSorry)
        {
            var timeText = string.IsNullOrWhiteSpace(selectedDelay) ? "InputTime" : selectedDelay;
            if (string.Equals(timeText, "すみません", StringComparison.Ordinal))
            {
                timeText = "InputTime";
            }

            var message = timeText + "以内に配達します。";
            if (withSorry)
            {
                message = "すみません。\n" + message;
            }

            return message;
        }

        private void OpenMailClient(string to, string subject, string body)
        {
            try
            {
                var mailto = "mailto:" + Uri.EscapeDataString(to)
                    + "?subject=" + Uri.EscapeDataString(subject)
                    + "&body=" + Uri.EscapeDataString(body);
                Process.Start(mailto);
            }
            catch
            {
                MessageBox.Show("メールアプリを開けませんでした。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ShowKitchenPasswordDialog()
        {
            using (var dialog = new Form())
            using (var label = new Label())
            using (var txtPassword = new TextBox())
            using (var btnOk = new Button())
            using (var btnCancel = new Button())
            {
                dialog.Text = "Kitchen Password";
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.ClientSize = new System.Drawing.Size(280, 130);

                label.Text = "Password:";
                label.AutoSize = true;
                label.Location = new System.Drawing.Point(12, 15);

                txtPassword.Location = new System.Drawing.Point(15, 38);
                txtPassword.Size = new System.Drawing.Size(250, 25);
                txtPassword.UseSystemPasswordChar = true;

                btnOk.Text = "OK";
                btnOk.DialogResult = DialogResult.OK;
                btnOk.Location = new System.Drawing.Point(109, 80);
                btnOk.Size = new System.Drawing.Size(75, 30);

                btnCancel.Text = "Cancel";
                btnCancel.DialogResult = DialogResult.Cancel;
                btnCancel.Location = new System.Drawing.Point(190, 80);
                btnCancel.Size = new System.Drawing.Size(75, 30);

                dialog.Controls.Add(label);
                dialog.Controls.Add(txtPassword);
                dialog.Controls.Add(btnOk);
                dialog.Controls.Add(btnCancel);
                dialog.AcceptButton = btnOk;
                dialog.CancelButton = btnCancel;

                return dialog.ShowDialog(this) == DialogResult.OK
                    && txtPassword.Text == KitchenPassword;
            }
        }

        private void ShowPage(string pageName)
        {
            if (pageName == "Kitchen")
            {
                ApplyKitchenPcLayout();
            }
            else
            {
                ApplyDefaultMobileLayout();
            }

            pnlCustomer.Visible = pageName == "Customer";
            pnlKitchen.Visible = pageName == "Kitchen";
            pnlDelivery.Visible = pageName == "Delivery";
        }

        private void ApplyDefaultMobileLayout()
        {
            ClientSize = new Size(390, 760);

            btnPageCustomer.Location = new Point(10, 10);
            btnPageKitchen.Location = new Point(130, 10);
            btnPageDelivery.Location = new Point(250, 10);

            pnlCustomer.Location = new Point(10, 50);
            pnlCustomer.Size = new Size(370, 700);
            pnlDelivery.Location = new Point(10, 50);
            pnlDelivery.Size = new Size(370, 700);

            pnlKitchen.Location = new Point(10, 50);
            pnlKitchen.Size = new Size(370, 700);
            pnlKitchen.AutoScroll = true;

            dgvKitchenMenu.Location = new Point(20, 145);
            dgvKitchenMenu.Size = new Size(330, 220);
            dgvKitchenOrders.Location = new Point(20, 375);
            dgvKitchenOrders.Size = new Size(330, 300);
        }

        private void ApplyKitchenPcLayout()
        {
            ClientSize = new Size(1200, 760);

            btnPageCustomer.Location = new Point(10, 10);
            btnPageKitchen.Location = new Point(130, 10);
            btnPageDelivery.Location = new Point(250, 10);

            pnlKitchen.Location = new Point(10, 50);
            pnlKitchen.Size = new Size(ClientSize.Width - 20, ClientSize.Height - 60);
            pnlKitchen.AutoScroll = true;

            int panelWidth = pnlKitchen.Width;
            int contentWidth = panelWidth - 40;

            lblKitchen.Location = new Point(20, 20);

            EnsureKitchenReceiptButton();
            btnKitchenPrintSimpleReceipt.Location = new Point(contentWidth - 188, 20);
            btnKitchenPrintReceipt.Location = new Point(contentWidth - 90, 20);

            dgvKitchenOrders.Location = new Point(20, 60);
            dgvKitchenOrders.Size = new Size(contentWidth, 280);

            lblKitchenMenuName.Location = new Point(20, 360);
            txtKitchenMenuName.Location = new Point(63, 356);
            txtKitchenMenuName.Size = new Size(240, 25);

            lblKitchenMenuPrice.Location = new Point(320, 360);
            txtKitchenMenuPrice.Location = new Point(360, 356);
            txtKitchenMenuPrice.Size = new Size(90, 25);

            chkKitchenRecommended.Location = new Point(460, 358);
            lblKitchenMenuType.Location = new Point(20, 360);
            cmbKitchenMenuType.Location = new Point(0, 0);
            cmbKitchenMenuType.Size = new Size(0, 0);

            btnUploadPhoto.Location = new Point(555, 354);
            btnUploadPhoto.Size = new Size(90, 30);
            btnAddMenu.Location = new Point(651, 354);
            btnAddMenu.Size = new Size(110, 30);
            lblKitchenPhotoPath.Location = new Point(20, 396);

            dgvKitchenMenu.Location = new Point(20, 426);
            dgvKitchenMenu.Size = new Size(contentWidth, 350);

            pnlKitchen.AutoScrollMinSize = new Size(0, 800);
        }

        private void EnsureKitchenReceiptButton()
        {
            if (btnKitchenPrintReceipt == null)
            {
                btnKitchenPrintReceipt = new Button
                {
                    Name = "btnKitchenPrintReceipt",
                    Text = "領収書",
                    Size = new Size(90, 30),
                    BackColor = Color.FromArgb(13, 110, 253),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnKitchenPrintReceipt.FlatAppearance.BorderSize = 0;
                btnKitchenPrintReceipt.Click += btnKitchenPrintReceipt_Click;
                pnlKitchen.Controls.Add(btnKitchenPrintReceipt);
                btnKitchenPrintReceipt.BringToFront();
            }

            if (btnKitchenPrintSimpleReceipt == null)
            {
                btnKitchenPrintSimpleReceipt = new Button
                {
                    Name = "btnKitchenPrintSimpleReceipt",
                    Text = "リシート",
                    Size = new Size(90, 30),
                    BackColor = Color.FromArgb(13, 110, 253),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnKitchenPrintSimpleReceipt.FlatAppearance.BorderSize = 0;
                btnKitchenPrintSimpleReceipt.Click += btnKitchenPrintSimpleReceipt_Click;
                pnlKitchen.Controls.Add(btnKitchenPrintSimpleReceipt);
                btnKitchenPrintSimpleReceipt.BringToFront();
            }

            btnKitchenPrintReceipt.Visible = true;
            btnKitchenPrintSimpleReceipt.Visible = true;
        }

        private void UpdateDeliveryInfo()
        {
            var selectedOrder = GetSelectedDeliveryOrder();
            if (selectedOrder == null)
            {
                lblDeliveryAddress.Text = "-";
                lblDeliveryPrice.Text = "-";
                UpdateEmbeddedMap(StoreAddress, string.Empty);
                return;
            }

            lblDeliveryAddress.Text = selectedOrder.Address;

            var totalPriceByAddress = orderList
                .Where(o => string.Equals(o.Address, selectedOrder.Address, StringComparison.OrdinalIgnoreCase))
                .Sum(o => o.PriceYen);

            lblDeliveryPrice.Text = totalPriceByAddress.ToString() + " yen";
            var changeLines = BuildChangeLines(totalPriceByAddress);
            if (changeLines.Length > 0)
            {
                lblDeliveryPrice.Text += "\n" + string.Join("\n", changeLines);
            }

            UpdateEmbeddedMap(StoreAddress, selectedOrder.Address);
        }

        private OrderItem GetSelectedDeliveryOrder()
        {
            if (dgvDeliveryOrders.CurrentRow?.DataBoundItem is OrderItem order)
            {
                return order;
            }

            return null;
        }

        private void UpdateEmbeddedMap(string originAddress, string destinationAddress)
        {
            if (string.IsNullOrWhiteSpace(destinationAddress))
            {
                wbDeliveryMap.DocumentText = "<html><body style='font-family:Segoe UI;font-size:12px;'>住所を選択すると地図を表示します。</body></html>";
                return;
            }

            var mapUrl = "https://maps.google.com/maps?saddr="
                + Uri.EscapeDataString(originAddress)
                + "&daddr=" + Uri.EscapeDataString(destinationAddress)
                + "&output=embed";

            wbDeliveryMap.Navigate(mapUrl);
        }

        private string[] BuildChangeLines(int total)
        {
            if (total <= 0)
            {
                return new string[0];
            }

            var baseAmounts = new[] { 2000, 5000, 10000 };
            return baseAmounts
                .Where(amount => amount >= total)
                .Select(amount => amount + " → お釣り " + (amount - total) + " 円")
                .ToArray();
        }

        private int ExtractPriceYen(string itemText)
        {
            if (string.IsNullOrWhiteSpace(itemText))
            {
                return 0;
            }

            string digits = string.Empty;
            foreach (char ch in itemText)
            {
                if (char.IsDigit(ch))
                {
                    digits += ch;
                }
                else if (digits.Length > 0)
                {
                    break;
                }
            }

            if (int.TryParse(digits, out int price))
            {
                return price;
            }

            return 0;
        }
    }
}
