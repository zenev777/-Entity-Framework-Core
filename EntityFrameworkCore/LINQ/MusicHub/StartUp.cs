namespace MusicHub
{
    using System;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 9));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var albumInfo = context.Producers
                .Include(a=>a.Albums)
                .ThenInclude(s=>s.Songs)
                .ThenInclude(w=>w.Writer)
                .First(p => p.Id == producerId).Albums
                .Select(x=> new
                {
                    AlbumName = x.Name,
                    x.ReleaseDate,
                    ProducerName = x.Producer?.Name,
                    AlbumSongs = x.Songs.Select(s => new
                        {
                            SongName = s.Name,
                            SongPrice = s.Price,
                            SongWriterName = s.Writer.Name
                        })
                        .OrderByDescending(sn=>sn.SongName)
                        .ThenBy(x=>x.SongWriterName),
                    TotalAlbumPrice = x.Price
                }).OrderByDescending(a=>a.TotalAlbumPrice);

            foreach (var album in albumInfo)
            {
                sb
                    .AppendLine($"-AlbumName: {album.AlbumName}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine("-Songs:");

                int counter = 1;
                foreach (var song in album.AlbumSongs)
                {
                    sb
                        .AppendLine($"---#{counter++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.SongPrice:f2}")
                        .AppendLine($"---Writer: {song.SongWriterName}");
                }
                sb
                    .AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:f2}");
            }


            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 1;

            //!!! Doesnt Work

            //var songsAboveDuration = context.Songs
            //    .Include(sp=>sp.SongPerformers)
            //        .ThenInclude(sp=>sp.Performer)
            //    .Include(w=>w.Writer)
            //    .Include(a=>a.Album)
            //        .ThenInclude(p=>p.Producer)
            //    .ToList()
            //    .Where(s => s.Duration.Seconds > duration)
            //    .Select(s => new
            //    {
            //        SongName = s.Name,
            //        PerformerFullName = s.SongPerformers
            //            .Select(sp => sp.Performer.FirstName + " " + sp.Performer.LastName)
            //            .OrderBy(name => name)
            //            .ToList(),
            //        WriterName = s.Writer.Name,
            //        AlbumProducerName = s.Album.Producer.Name,
            //        Duration = s.Duration.ToString("c")
            //    })
            //    .OrderBy(s => s.SongName)
            //    .ThenBy(s => s.WriterName)
            //    .ToList();


            //foreach (var s in songsAboveDuration)
            //{
            //    sb
            //        .AppendLine($"-Song #{counter++}")
            //        .AppendLine($"---SongName: {s.SongName}")
            //        .AppendLine($"---Writer: {s.WriterName}");

            //    if (s.PerformerFullName.Any())
            //    {
            //        sb.AppendLine(string
            //            .Join(Environment.NewLine, s.PerformerFullName
            //                .Select(p => $"---Performer: {p}")));
            //    }

            //    sb
            //        .AppendLine($"---AlbumProducer: {s.AlbumProducerName}")
            //        .AppendLine($"---Duration: {s.Duration}");
            //}

            return sb.ToString().TrimEnd();
        }
    }
}
