using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
//Librerias de NAudio (Corre el proyecto una vez para que se traiga las librerias)
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
//Clase nueva para el timer del programa 
using System.Windows.Threading;

namespace Reproductor
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;

        //Lector de archivos
        AudioFileReader reader;
        //Comunicacion conla tarjeta de audio
        //Exclusivo para salidas
        WaveOut output;
        string path = Environment.CurrentDirectory;

        int i = 0;

        int[] Tiempo = { 4000, 5500, 6000, 7000, 8000, 9250, 10000, 11000, 12000, 13000, 14000, 16000, 17000, 18000, 19000, 21000, 23000, 23900, 26000, 29000, 30000, 31000, 34000, 35000, 37000, 39000, 41000, 42000, 43000, 45000, 46000, 49000, 50000, 52000, 53000, 54000, 59000  };
        string[] Letra = {"Yo tengo mis bases",
                          "en todos los países",
                          "si tiro una bomba",
                          "no creo que avise",
                          "esto es blanco y negro",
                          "yo no veo grises",
                          "si no explotó todo",
                          "fue por que no quise",
                          "yo te dije",
                          "hey, dont call me pato",
                          "lo dijiste igual",
                          "you know what?",
                          "ahora desato",
                          "una guerra mundial",
                          "soy donald trump",
                          "you better run",
                          "si no hacen caso",
                          "misilazo para Irán",
                          "Soy donald trump",
                          "you better run",
                          "si no hacen caso",
                          "misilazo para Irán",
                          "si tiro una bomba",
                          "no creo que avise",
                          "Si no explotó todo",
                          "fue porque no quise",
                          "yo no soy pato", 
                          "yo no empato",
                          "yo la gano",
                          "me dan un rato",
                          "después mato al koreano",
                          "si me dicen pato",
                          "les tiro un misilazo",
                          "ok?",
                          "pum misilazo, no me digan pato",
                          "no tengo nombre de pato, me dicen pato pum lo mato, misilazo"
                            };

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            
            timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
           if(reader.CurrentTime.TotalMilliseconds > Tiempo[i])
            {
                Lyrics.Text = Letra[i];
                i++;
            }
        }

        private void BtnReproducir_Click(object sender, RoutedEventArgs e)
        {
            if (output != null && output.PlaybackState == PlaybackState.Paused)
            {
                //Retomo reproduccion
                output.Play();
                btnReproducir.IsEnabled = false;

            }
            else
            {
                Lyrics.Visibility = Visibility.Visible;
                btnReproducir.Visibility = Visibility.Hidden;
                Pb.Visibility = Visibility.Visible;
                
                    reader = new AudioFileReader(path + "/DonaldTrump.mp3");
                    output = new WaveOut();
             

                    output.PlaybackStopped += Output_PlaybackStopped;
                    output.Init(reader);
                    output.Play();
                    timer.Start();

               
            }
        }

        private void Output_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            reader.Dispose();
            output.Dispose();
            timer.Stop();
        }
    }
}
