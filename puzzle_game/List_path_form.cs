using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle_game
{
    public partial class List_path_form : System.Windows.Forms.Form
    {
        public List_path_form(Form form)
        {
            InitializeComponent();

            this.form = form;
            // 重新設定視窗位置
            set_form_loction();
            // 將結果加到List
            add_plan_result();
        }

        private Form form;

        private void set_form_loction()
        {
            Point form_loc = form.Location;
            Size form_size = form.Size;
            this.Location = new Point(form_loc.X + form_size.Width - 15, form_loc.Y);
            this.Height = form_size.Height;
        }

        private void add_plan_result()
        {
            object plan_result = form.get_plan_result();
            int index = 1;
            foreach (var path in (List<Node>)plan_result)
            {
                ListViewItem item = new ListViewItem("Step " + index);
                item.SubItems.Add("空格往" + path.direction);
                list.Items.Add(item);
                index++;
            }
        }
    }
}
