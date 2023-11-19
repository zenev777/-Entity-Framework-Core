using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models
{
    public class SongPerformer
    {
        [ForeignKey(nameof(SongId))]
        public int SongId { get; set; }
        public Song Song { get; set; }

        [ForeignKey(nameof(PerformerId))]
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
    }
}