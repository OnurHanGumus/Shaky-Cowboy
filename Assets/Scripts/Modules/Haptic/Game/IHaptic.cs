using RichTap.Common;

public interface IHaptic
{
    void SetAmplitude(float value);
    void SetPreset(RichtapPreset value);
    void CheckBeforePlay(HapticEnums value);
    int HapticMode { get; set; }
}