/*
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GestorProgreso : MonoBehaviour
{
    public static GestorProgreso Instancia;

    public DatosJugador datosJugador;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CargarDatos();
    }

    public void GuardarDatos()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string ruta = Application.dataPath + "/progreso.dat"; // dataPath para la carpeta del juego. Si utilizase persistentDataPath lo guardaria en el sistema.
        FileStream stream = new FileStream(ruta, FileMode.Create);

        formatter.Serialize(stream, datosJugador);
        stream.Close();
    }

    public void CargarDatos()
    {
        string ruta = Application.dataPath + "/progreso.dat"; 
        if (File.Exists(ruta))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(ruta, FileMode.Open);

            datosJugador = formatter.Deserialize(stream) as DatosJugador;
            stream.Close();
        }
        else
        {
            datosJugador = new DatosJugador();
            datosJugador.puntuacionesMaximas = new int[8]; // Ajusta el tamaño según el número de niveles
            datosJugador.nivelesCompletados = new bool[8]; // Ajusta el tamaño según el número de niveles 
            datosJugador.dificultad = 1; // Añadido: Dificultad predeterminada (normal)
        }
        
    }
}
*/
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class GestorProgreso : MonoBehaviour
{
    public static GestorProgreso Instancia;
    public DatosJugador datosJugador;
    public List<DatosNivel> datosNivel = new List<DatosNivel>();

    private string rutaJugador;
    private string rutaNivel;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Definir rutas de guardado según la plataforma
        #if UNITY_ANDROID
            rutaJugador = Path.Combine(Application.persistentDataPath, "progresoJugador.dat");
            rutaNivel = Path.Combine(Application.persistentDataPath, "progresoNivel.dat");
        #else
            rutaJugador = Application.dataPath + "/progresoJugador.dat";
            rutaNivel = Application.dataPath + "/progresoNivel.dat";
        #endif

        CargarDatos();
    }

    public void GuardarDatos()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // Guardar DatosJugador
        FileStream streamJugador = new FileStream(rutaJugador, FileMode.Create);
        formatter.Serialize(streamJugador, datosJugador);
        streamJugador.Close();

        // Guardar DatosNivel
        FileStream streamNivel = new FileStream(rutaNivel, FileMode.Create);
        formatter.Serialize(streamNivel, datosNivel);
        streamNivel.Close();
    }

    public void CargarDatos()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // Cargar DatosJugador
        if (File.Exists(rutaJugador))
        {
            FileStream streamJugador = new FileStream(rutaJugador, FileMode.Open);
            datosJugador = formatter.Deserialize(streamJugador) as DatosJugador;
            streamJugador.Close();
        }
        else
        {
            datosJugador = new DatosJugador();
            datosJugador.puntuacionesMaximas = new int[8];
            datosJugador.nivelesCompletados = new bool[8];
            datosJugador.dificultadesCompletadas = new string[8];
        }

        // Cargar DatosNivel
        if (File.Exists(rutaNivel))
        {
            FileStream streamNivel = new FileStream(rutaNivel, FileMode.Open);
            datosNivel = formatter.Deserialize(streamNivel) as List<DatosNivel>;
            streamNivel.Close();
        }
        else
        {
            datosNivel = new List<DatosNivel>();
            for (int i = 0; i < 8; i++)
            {
                datosNivel.Add(new DatosNivel());
            }
        }
    }

    public void GuardarDificultadNivel(int nivel, string dificultad)
    {
        datosJugador.dificultadesCompletadas[nivel] = dificultad;
        GuardarDatos();
    }
}