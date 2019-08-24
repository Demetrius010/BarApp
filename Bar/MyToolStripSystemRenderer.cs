using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bar
{
    class MyToolStripSystemRenderer : System.Windows.Forms.ToolStripRenderer
    {
        public MyToolStripSystemRenderer() { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            //Making this non-op removes the artifact line that is typically drawn on the bottom edge
            //base.OnRenderToolStripBorder(e);
        }
    }
}
