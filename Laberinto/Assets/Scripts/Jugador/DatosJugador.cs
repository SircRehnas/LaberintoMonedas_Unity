using System;

[System.Serializable]
public class DatosJugador
{
    public int[] puntuacionesMaximas; // Puntuación máxima por nivel
    public bool[] nivelesCompletados; // Estado de completado por nivel
    public string[] dificultadesCompletadas; // Dificultad completada por nivel
}