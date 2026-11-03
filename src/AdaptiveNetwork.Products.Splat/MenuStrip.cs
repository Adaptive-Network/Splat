using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;

using System;
using System.Linq;
using System.Windows.Forms;

namespace AdaptiveNetwork.Products.Splat
{
    /// <summary>
    /// Provides a GH_AssemblyPriority that injects a 'Splat' menu item into the host application's File menu to close
    /// all documents and exit Grasshopper. 
    /// </summary>
    /// <remarks>Registers a canvas-created handler during assembly loading. When a canvas is created, locates
    /// the main menu's File ToolStripMenuItem and inserts a 'Splat' ToolStripMenuItem (with tooltip, name, and image)
    /// before the last two items. The menu item's click handler removes all documents, closes the document editor,
    /// unloads objects, and reloads memory assemblies.</remarks>
    public sealed class MenuStrip : GH_AssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            Instances.CanvasCreated += OnCanvasCreated;
            return GH_LoadingInstruction.Proceed;
        }

        private static void OnCanvasCreated(GH_Canvas canvas)
        {
            GH_DocumentEditor editor = Instances.DocumentEditor;
            if (editor?.MainMenuStrip is null) return;
            ToolStripMenuItem fileMenu = editor.MainMenuStrip.Items.OfType<ToolStripMenuItem>()
                .SingleOrDefault(dd => dd.Text == "File");
            if (fileMenu is null) return;
            var item = new ToolStripMenuItem("Splat");
            item.ToolTipText = "Close all documents and exit Grasshopper";
            item.Name = "Splat";
            item.Click += OnSplatClicked;
            item.Image = Properties.Resources.splat_16;
            if (fileMenu.DropDownItems.ContainsKey("Splat")) return;
            int length = fileMenu.DropDownItems.Count;
            fileMenu.DropDownItems.Insert(length - 2, item);
        }

        private static void OnSplatClicked(object sender, EventArgs e)
        {
            Instances.DocumentServer.RemoveAllDocuments();
            Instances.DocumentEditor.CloseForReal();
            Instances.UnloadAllObjects();
            Instances.ReloadMemoryAssemblies();
        }
    }
}
