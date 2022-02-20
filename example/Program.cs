namespace MidiFileTests
{
    using System;
    using System.Threading;
    using System.Windows;
    using MidiParser;

    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.WriteLine("Enter filename");

            var path = Console.ReadLine();

            Console.WriteLine("Parsing: {0}\n", path);

            var midiFile = new MidiFile(path);

            Console.WriteLine("Format: {0}", midiFile.Format);
            Console.WriteLine("TicksPerQuarterNote: {0}", midiFile.TicksPerQuarterNote);
            Console.WriteLine("TracksCount: {0}", midiFile.TracksCount);

            var json = "[=[[";

            foreach (var track in midiFile.Tracks)
            {
                foreach (var ev in track.MidiEvents)
                {
                    if (ev.MidiEventType != MidiEventType.NoteOn)
                    {
                        continue;
                    }

                    json += $"{{\"time\":\"{ev.Time}\",\"note\":\"{ev.Note}\"}},";
                }
            }

            json = json.Substring(0, json.Length - 1);

            json += "]]=],";

            Clipboard.SetText(json);
            Console.Write(json);

            /*foreach (var track in midiFile.Tracks)
            {
                Console.WriteLine("\nTrack: {0}\n", track.Index);

                foreach (var midiEvent in track.MidiEvents)
                {
                    const string Format = "{0} Channel {1} Time {2} Args {3} {4}";
                    if (midiEvent.MidiEventType == MidiEventType.MetaEvent)
                    {
                        Console.WriteLine(
                            Format,
                            midiEvent.MetaEventType,
                            "-",
                            midiEvent.Time,
                            midiEvent.Arg2,
                            midiEvent.Arg3);
                    }
                    else
                    {
                        Console.WriteLine(
                            Format,
                            midiEvent.MidiEventType,
                            midiEvent.Channel,
                            midiEvent.Time,
                            midiEvent.Arg2,
                            midiEvent.Arg3);
                    }
                }
            }*/
        }
    }
}