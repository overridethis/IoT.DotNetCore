namespace IoT.DotNetCore.Hardware
{
    public interface IBuzzer : IHardware
    {
        /// <summary>
        /// Produces a tone for the specified ms.
        /// </summary>
        /// <param name="time">Milliseconds</param>
        void Tone(int time);
    }
}