namespace Kalimbra.Instruments
{
    public interface IInstrument
    {
        short Play(float frequency, int time);
    }
}