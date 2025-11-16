using System.Diagnostics;

namespace Proyecto.UI.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Esta es la propiedad que nos interesa
        public string? Message { get; set; }
    }
}