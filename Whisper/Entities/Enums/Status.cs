using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Whisper.Entities.Enums
{
    // Proje Ekleme sayfası için; durum kısmında belirtilecek olan 'aktif pasif' tanımlaması yapıldı.
    public enum Status
    {
        [Display(Name = "Aktif")]
        Active,
        [Display(Name = "Pasif")]
        Passive,
        [Display(Name = "Donduruldu")]
        Frozen


    }
}
