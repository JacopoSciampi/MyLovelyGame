public class RaceDto
{
    public string name { get; set; }
    public string icon { get; set; }

    RaceDto(RaceDto data)
    {
        name = data.name;
        icon = data.icon;
    }
}
