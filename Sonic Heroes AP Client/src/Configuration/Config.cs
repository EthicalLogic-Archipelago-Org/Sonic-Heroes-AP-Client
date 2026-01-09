using System.ComponentModel;
using Reloaded.Mod.Interfaces.Structs;
using Sonic_Heroes_AP_Client.Template.Configuration;

namespace Sonic_Heroes_AP_Client.Configuration;

public class Config : Configurable<Config>
{
    /*
        User Properties:
            - Please put all of your configurable properties here.

        By default, configuration saves as "Config.json" in mod user config folder.
        Need more config files/classes? See Configuration.cs

        Available Attributes:
        - Category
        - DisplayName
        - Description
        - DefaultValue

        // Technically Supported but not Useful
        - Browsable
        - Localizable

        The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
    */
    
    [Category("Test")]
    [DisplayName("Play Item and Ring Link Sounds")]
    [Description("Plays sounds when receiving items or Ring Link packets. If Sounds are Crashing, disable them here.")]
    [DefaultValue(true)]
    public bool PlaySounds { get; set; } = true;
    
    
    //[DisplayName("Test Option Here")]
    //[Description("Test.")]
    //[DefaultValue(true)]
    //public bool TestOption { get; set; } = true;
    
    
    [DisplayName("AP Connection Options")]
    [Description("AP Connection Options")]
    public ConnectionOptions ConnectionOptions { get; set; } = new ConnectionOptions();
    
    
    [DisplayName("Tag Options")]
    [Description("Tag Options")]
    public TagOptions TagOptions { get; set; } = new TagOptions();
    
    
    [DisplayName("In-Game Log Options")]
    [Description("In-Game Log Options")]
    public InGameLogOptions InGameLogOptions { get; set; } = new InGameLogOptions();


    [DisplayName("Music Shuffle Options")]
    [Description("Music Shuffle Options")]
    public MusicShuffleOptions MusicShuffleOptions { get; set; } = new MusicShuffleOptions();
    
    
    [DisplayName("Heroes Music Shuffle Options")]
    [Description("Heroes Music Shuffle Options")]
    public HeroesMusicShuffleOptions HeroesMusicShuffleOptions { get; set; } = new HeroesMusicShuffleOptions();
    
    
    [DisplayName("SA2 Music Shuffle Options")]
    [Description("SA2 Music Shuffle Options")]
    public SA2MusicShuffleOptions SA2MusicShuffleOptions { get; set; } = new SA2MusicShuffleOptions();
    
    
    [DisplayName("SADX Music Shuffle Options")]
    [Description("SADX Music Shuffle Options")]
    public SADXMusicShuffleOptions SADXMusicShuffleOptions { get; set; } = new SADXMusicShuffleOptions();
    
    
    [DisplayName("Custom Music Shuffle Options")]
    [Description("Custom Music Shuffle Options")]
    public CustomMusicShuffleOptions CustomMusicShuffleOptions { get; set; } = new CustomMusicShuffleOptions();
}

public class ConnectionOptions
{
    [DisplayName("Host IP")]
    [Description("Host address of the Archipelago server")]
    [DefaultValue("Archipelago.gg")]
    public string Server { get; set; } = "Archipelago.gg";
    
    [DisplayName("Port")]
    [Description("Port open for the Archipelago server")]
    [DefaultValue("55555")]
    public int Port { get; set; } = 55555;

    [DisplayName("Slot")]
    [Description("Slot user name used to connect to the Archipelago server")]
    [DefaultValue("Sonic")]
    public string Slot { get; set; } = "Sonic";

    [DisplayName("Password")]
    [Description("Password for the Archipelago server")]
    [DefaultValue("")]
    public string Password { get; set; } = "";
}

public class TagOptions
{
    [DisplayName("Death Link")]
    [Description("Set how death link will work in the Archipelago session")]
    [DefaultValue(true)]
    public bool DeathLink { get; set; } = true;

    
    
    [DisplayName("Ring Link")]
    [Description("Set how ring link will work in the Archipelago session")]
    [DefaultValue(true)]
    public bool RingLink { get; set; } = true;
}

public class InGameLogOptions
{
    [DisplayName("Log Message Time")]
    [Description("Set how long each message will display for in seconds in the log")]
    [DefaultValue(5)]
    public int LogMessageTime { get; set; } = 5;
    

    [DisplayName("Log Message Count")]
    [Description("Set how many messages can be displayed in the log at once.")]
    [DefaultValue(6)]
    public int LogMessageCount { get; set; } = 6;
    
    
    [DisplayName("Log Message Delay")]
    [Description("Set how long (in ms) between log messages appearing.")]
    [DefaultValue(750)]
    public int LogMessageDelay { get; set; } = 750;
}

public class MusicShuffleOptions
{
    [DisplayName("Music Shuffle")]
    [Description("Set music shuffle")]
    [DefaultValue(false)]
    public bool MusicShuffle { get; set; } = false;
    
    
    [DisplayName("Separate Boss Music")]
    [Description("Shuffle Boss Music Separately from All Music (Requires Additional Folder for Custom)\nThere are 11 Boss Tracks.")]
    [DefaultValue(true)]
    public bool MusicShuffleBossMusic { get; set; } = false;
    
    
    [DisplayName("Separate Menu Music")]
    [Description("Shuffle Menu Music Separately from All Music (Requires Additional Folder for Custom)\nThere are 4 Menu Tracks.")]
    [DefaultValue(true)]
    public bool MusicShuffleMenuMusic { get; set; } = false;
    
    
    [DisplayName("Separate Short Music")]
    [Description("Shuffle Short Music Tracks (Like 2P Battle Stages) Separately from All Music (Requires Additional Folder for Custom)\nThere are 5 Short Tracks.")]
    [DefaultValue(true)]
    public bool MusicShuffleShortMusic { get; set; } = false;
    
    
    [DisplayName("Separate Long Jingles")]
    [Description("Shuffle Long Jingles (Non-Looping) Separately from Jingles (Requires Additional Folder for Custom)\nThere are 12 Long Jingles.")]
    [DefaultValue(true)]
    public bool MusicShuffleLongJingle { get; set; } = false;
    
}

public class HeroesMusicShuffleOptions
{
    [DisplayName("Music Shuffle Heroes")]
    [Description("Use Sonic Heroes music in shuffle")]
    [DefaultValue(false)]
    public bool MusicShuffleHeroes { get; set; } = false;
    
    
    [DisplayName("Heroes BGM Folder")]
    [Description("The Location of The BGM Folder in dvdroot of Sonic Heroes (/dvdroot/bgm/)")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: true,
        title: "Custom Music Folder",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "SHAPCustomMusic",
        multiSelect: false,
        forceFileSystem: true)]
    public string MusicShuffleHeroesBGMFolder { get; set; } = "";
}

public class SA2MusicShuffleOptions
{
    [DisplayName("Music Shuffle SA2")]
    [Description("Use Sonic Adventure 2 music in shuffle (requires extra setup)")]
    [DefaultValue(false)]
    public bool MusicShuffleSA2 { get; set; } = false;
    
    
    
    [DisplayName("SA2 ADX Folder")]
    [Description("The Location of The ADX Folder in gd_PC in SA2 (/resource/gd_PC/ADX/)")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: true,
        title: "Custom Music Folder",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "SHAPCustomMusic",
        multiSelect: false,
        forceFileSystem: true)]
    public string MusicShuffleSA2ADXFolder { get; set; } = "";
}

public class SADXMusicShuffleOptions
{
    [DisplayName("Music Shuffle SADX")]
    [Description("Use Sonic Adventure DX music in shuffle (requires extra setup)")]
    [DefaultValue(false)]
    public bool MusicShuffleSADX { get; set; } = false;
    
    
    
    [DisplayName("SADX WMA Folder")]
    [Description("The Location of The WMA Folder (/SoundData/bgm/wma)")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: true,
        title: "Custom Music Folder",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "SHAPCustomMusic",
        multiSelect: false,
        forceFileSystem: true)]
    public string MusicShuffleSADXWMAFolder { get; set; } = "";
}

public class CustomMusicShuffleOptions
{
    [DisplayName("Music Shuffle Custom")]
    [Description("Use custom music in shuffle (requires extra setup)\nThere are 18 Music Tracks and 18 Jingles in addition to the other types as well.")]
    [DefaultValue(false)]
    public bool MusicShuffleCustom { get; set; } = false;
    
    
    
    [DisplayName("Music Shuffle Custom Folder")]
    [Description("Set folder for custom music (requires extra setup)")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: true,
        title: "Custom Music Folder",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "SHAPCustomMusic",
        multiSelect: false,
        forceFileSystem: true)]
    public string MusicShuffleCustomFolder { get; set; } = "";
}





/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}




/*
    [DisplayName("Music Shuffle")]
    [Description("Set music shuffle (requires extra setup)")]
    [DefaultValue(false)]
    public bool MusicShuffleExtra { get; set; } = false;

    [DisplayName("Folder Picker")]
    [Description("Opens a file picker but locked to only allow folder selections.")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: false,
        title: "Custom Folder Select",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "ModFolder",
        multiSelect: true,
        forceFileSystem: true)]
    public string Folder { get; set; } = "";
/*

    /*
    [DisplayName("Int Slider")]
    [Description("This is a int that uses a slider control similar to a volume control slider.")]
    [DefaultValue(100)]
    [SliderControlParams(
        minimum: 0.0,
        maximum: 100.0,
        smallChange: 1.0,
        largeChange: 10.0,
        tickFrequency: 10,
        isSnapToTickEnabled: false,
        tickPlacement: SliderControlTickPlacement.BottomRight,
        showTextField: true,
        isTextFieldEditable: true,
        textValidationRegex: "\\d{1-3}")]
    public int IntSlider { get; set; } = 100;

    [DisplayName("Double Slider")]
    [Description("This is a double that uses a slider control without any frills.")]
    [DefaultValue(0.5)]
    [SliderControlParams(minimum: 0.0, maximum: 1.0)]
    public double DoubleSlider { get; set; } = 0.5;

    [DisplayName("File Picker")]
    [Description("This is a sample file picker.")]
    [DefaultValue("")]
    [FilePickerParams(title: "Choose a File to load from")]
    public string File { get; set; } = "";

    [DisplayName("Folder Picker")]
    [Description("Opens a file picker but locked to only allow folder selections.")]
    [DefaultValue("")]
    [FolderPickerParams(
        initialFolderPath: Environment.SpecialFolder.Desktop,
        userCanEditPathText: false,
        title: "Custom Folder Select",
        okButtonLabel: "Choose Folder",
        fileNameLabel: "ModFolder",
        multiSelect: true,
        forceFileSystem: true)]
    public string Folder { get; set; } = "";
    */