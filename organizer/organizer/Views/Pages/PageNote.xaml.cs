using organizer.Codes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace organizer.Views.Pages {
    /// <summary>
    /// Interaction logic for PageNote.xaml
    /// </summary>
    public partial class PageNote : Page {
        public PageNote(Note note) {
            InitializeComponent();
            this.note = note;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed) {
                // Package the data.
                DataObject data = new DataObject();
                data.SetData("Note", note);

                // Inititate the drag-and-drop operation.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        protected override void OnGiveFeedback(GiveFeedbackEventArgs e) {
            base.OnGiveFeedback(e);
            // These Effects values are set in the drop target's
            // DragOver event handler.
            if (e.Effects.HasFlag(DragDropEffects.Copy)) {
                Mouse.SetCursor(Cursors.Cross);
            } else if (e.Effects.HasFlag(DragDropEffects.Move)) {
                Mouse.SetCursor(Cursors.Pen);
            } else {
                Mouse.SetCursor(Cursors.No);
            }
            e.Handled = true;
        }

        protected override void OnRender(DrawingContext dc) {
            text.Text = note.text;
            base.OnRender(dc);
        }

        public Note note;
    }
}
