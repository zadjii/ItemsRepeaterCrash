using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI;

namespace ItemsRepeaterCrash
{

    public sealed class TagViewModel
    {
        public string Text { get; set; }
        public Color Color = Color.FromArgb(0, 255, 255, 255);
        public Brush BorderBrush => new SolidColorBrush(Color);
        public Brush TextBrush => new SolidColorBrush(Color.A == 0 ? Color.FromArgb(255, 255, 255, 255) : Color);
        public Brush BackgroundBrush => new SolidColorBrush(Color.A == 0 ? Color : Color.FromArgb((byte)(Color.A / 4), Color.R, Color.G, Color.B));

        public TagViewModel()
        {
        }
    }

    public sealed class ListItemViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public TagViewModel[] Tags { get; set; } = [];
        public bool HasTags => Tags.Length > 0;

        public ListItemViewModel()
        {

        }
    }

    public class Pokemon
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public string IconUrl => $"https://serebii.net/pokedex-sv/icon/new/{Number:D3}.png";

        public Pokemon(int number, string name, List<string> types)
        {
            Number = number;
            Name = name;
            Types = types;
        }

        public static readonly Dictionary<string, Color> TypeColors = new()
        {
            { "Normal", Color.FromArgb(255, 168, 168, 120) },   // Light Brownish Grey
            { "Fire", Color.FromArgb(255, 240, 128, 48) },      // Orange-Red
            { "Water", Color.FromArgb(255, 104, 144, 240) },    // Medium Blue
            { "Electric", Color.FromArgb(255, 248, 208, 48) },  // Yellow
            { "Grass", Color.FromArgb(255, 120, 200, 80) },     // Green
            { "Ice", Color.FromArgb(255, 152, 216, 216) },      // Cyan
            { "Fighting", Color.FromArgb(255, 192, 48, 40) },   // Red
            { "Poison", Color.FromArgb(255, 160, 64, 160) },    // Purple
            { "Ground", Color.FromArgb(255, 224, 192, 104) },   // Yellowish Brown
            { "Flying", Color.FromArgb(255, 168, 144, 240) },   // Light Blue
            { "Psychic", Color.FromArgb(255, 248, 88, 136) },   // Pink
            { "Bug", Color.FromArgb(255, 168, 184, 32) },       // Greenish Yellow
            { "Rock", Color.FromArgb(255, 184, 160, 56) },      // Brown
            { "Ghost", Color.FromArgb(255, 112, 88, 152) },     // Dark Purple
            { "Dragon", Color.FromArgb(255, 112, 56, 248) },    // Blue-Violet
            { "Dark", Color.FromArgb(255, 112, 88, 72) },       // Dark Brown
            { "Steel", Color.FromArgb(255, 184, 184, 208) },    // Light Grey
            { "Fairy", Color.FromArgb(255, 238, 153, 172) },    // Light Pink
        };
        // Method to get the color for a given type
        public static Color GetColorForType(string type)
        {
            // Check if the type exists in the dictionary
            if (TypeColors.TryGetValue(type, out Color color))
            {
                return color;
            }

            // Default color (e.g., white) if the type is not found
            return Color.FromArgb(255, 255, 255, 255);
        }
    }

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly ObservableCollection<ListItemViewModel> Items = new();

        public MainWindow()
        {
            this.InitializeComponent();

            foreach (Pokemon pkmn in _kanto)
            {
                Items.Add(GetPokemonListItem(pkmn));
            }
        }


        private static ListItemViewModel GetPokemonListItem(Pokemon pokemon)
        {
            return new ListItemViewModel()
            {
                Title = pokemon.Name,
                Subtitle = $"#{pokemon.Number}",
                Tags = pokemon.Types.Select(t => new TagViewModel() { Text = t, Color = Pokemon.GetColorForType(t) }).ToArray(),
            };
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
        }

        private readonly List<Pokemon> _kanto = new()
        {
            new Pokemon(1, "Bulbasaur", new List<string> { "Grass", "Poison" }),
            new Pokemon(2, "Ivysaur", new List<string> { "Grass", "Poison" }),
            new Pokemon(3, "Venusaur", new List<string> { "Grass", "Poison" }),
            new Pokemon(4, "Charmander", new List<string> { "Fire" }),
            new Pokemon(5, "Charmeleon", new List<string> { "Fire" }),
            new Pokemon(6, "Charizard", new List<string> { "Fire", "Flying" }),
            new Pokemon(7, "Squirtle", new List<string> { "Water" }),
            new Pokemon(8, "Wartortle", new List<string> { "Water" }),
            new Pokemon(9, "Blastoise", new List<string> { "Water" }),
            new Pokemon(10, "Caterpie", new List<string> { "Bug" }),
            new Pokemon(11, "Metapod", new List<string> { "Bug" }),
            new Pokemon(12, "Butterfree", new List<string> { "Bug", "Flying" }),
            new Pokemon(13, "Weedle", new List<string> { "Bug", "Poison" }),
            new Pokemon(14, "Kakuna", new List<string> { "Bug", "Poison" }),
            new Pokemon(15, "Beedrill", new List<string> { "Bug", "Poison" }),
            new Pokemon(16, "Pidgey", new List<string> { "Normal", "Flying" }),
            new Pokemon(17, "Pidgeotto", new List<string> { "Normal", "Flying" }),
            new Pokemon(18, "Pidgeot", new List<string> { "Normal", "Flying" }),
            new Pokemon(19, "Rattata", new List<string> { "Normal" }),
            new Pokemon(20, "Raticate", new List<string> { "Normal" }),
            new Pokemon(21, "Spearow", new List<string> { "Normal", "Flying" }),
            new Pokemon(22, "Fearow", new List<string> { "Normal", "Flying" }),
            new Pokemon(23, "Ekans", new List<string> { "Poison" }),
            new Pokemon(24, "Arbok", new List<string> { "Poison" }),
            new Pokemon(25, "Pikachu", new List<string> { "Electric" }),
            new Pokemon(26, "Raichu", new List<string> { "Electric" }),
            new Pokemon(27, "Sandshrew", new List<string> { "Ground" }),
            new Pokemon(28, "Sandslash", new List<string> { "Ground" }),
            new Pokemon(29, "Nidoran♀", new List<string> { "Poison" }),
            new Pokemon(30, "Nidorina", new List<string> { "Poison" }),
            new Pokemon(31, "Nidoqueen", new List<string> { "Poison", "Ground" }),
            new Pokemon(32, "Nidoran♂", new List<string> { "Poison" }),
            new Pokemon(33, "Nidorino", new List<string> { "Poison" }),
            new Pokemon(34, "Nidoking", new List<string> { "Poison", "Ground" }),
            new Pokemon(35, "Clefairy", new List<string> { "Fairy" }),
            new Pokemon(36, "Clefable", new List<string> { "Fairy" }),
            new Pokemon(37, "Vulpix", new List<string> { "Fire" }),
            new Pokemon(38, "Ninetales", new List<string> { "Fire" }),
            new Pokemon(39, "Jigglypuff", new List<string> { "Normal", "Fairy" }),
            new Pokemon(40, "Wigglytuff", new List<string> { "Normal", "Fairy" }),
            new Pokemon(41, "Zubat", new List<string> { "Poison", "Flying" }),
            new Pokemon(42, "Golbat", new List<string> { "Poison", "Flying" }),
            new Pokemon(43, "Oddish", new List<string> { "Grass", "Poison" }),
            new Pokemon(44, "Gloom", new List<string> { "Grass", "Poison" }),
            new Pokemon(45, "Vileplume", new List<string> { "Grass", "Poison" }),
            new Pokemon(46, "Paras", new List<string> { "Bug", "Grass" }),
            new Pokemon(47, "Parasect", new List<string> { "Bug", "Grass" }),
            new Pokemon(48, "Venonat", new List<string> { "Bug", "Poison" }),
            new Pokemon(49, "Venomoth", new List<string> { "Bug", "Poison" }),
            new Pokemon(50, "Diglett", new List<string> { "Ground" }),
            new Pokemon(51, "Dugtrio", new List<string> { "Ground" }),
            new Pokemon(52, "Meowth", new List<string> { "Normal" }),
            new Pokemon(53, "Persian", new List<string> { "Normal" }),
            new Pokemon(54, "Psyduck", new List<string> { "Water" }),
            new Pokemon(55, "Golduck", new List<string> { "Water" }),
            new Pokemon(56, "Mankey", new List<string> { "Fighting" }),
            new Pokemon(57, "Primeape", new List<string> { "Fighting" }),
            new Pokemon(58, "Growlithe", new List<string> { "Fire" }),
            new Pokemon(59, "Arcanine", new List<string> { "Fire" }),
            new Pokemon(60, "Poliwag", new List<string> { "Water" }),
            new Pokemon(61, "Poliwhirl", new List<string> { "Water" }),
            new Pokemon(62, "Poliwrath", new List<string> { "Water", "Fighting" }),
            new Pokemon(63, "Abra", new List<string> { "Psychic" }),
            new Pokemon(64, "Kadabra", new List<string> { "Psychic" }),
            new Pokemon(65, "Alakazam", new List<string> { "Psychic" }),
            new Pokemon(66, "Machop", new List<string> { "Fighting" }),
            new Pokemon(67, "Machoke", new List<string> { "Fighting" }),
            new Pokemon(68, "Machamp", new List<string> { "Fighting" }),
            new Pokemon(69, "Bellsprout", new List<string> { "Grass", "Poison" }),
            new Pokemon(70, "Weepinbell", new List<string> { "Grass", "Poison" }),
            new Pokemon(71, "Victreebel", new List<string> { "Grass", "Poison" }),
            new Pokemon(72, "Tentacool", new List<string> { "Water", "Poison" }),
            new Pokemon(73, "Tentacruel", new List<string> { "Water", "Poison" }),
            new Pokemon(74, "Geodude", new List<string> { "Rock", "Ground" }),
            new Pokemon(75, "Graveler", new List<string> { "Rock", "Ground" }),
            new Pokemon(76, "Golem", new List<string> { "Rock", "Ground" }),
            new Pokemon(77, "Ponyta", new List<string> { "Fire" }),
            new Pokemon(78, "Rapidash", new List<string> { "Fire" }),
            new Pokemon(79, "Slowpoke", new List<string> { "Water", "Psychic" }),
            new Pokemon(80, "Slowbro", new List<string> { "Water", "Psychic" }),
            new Pokemon(81, "Magnemite", new List<string> { "Electric", "Steel" }),
            new Pokemon(82, "Magneton", new List<string> { "Electric", "Steel" }),
            new Pokemon(83, "Farfetch'd", new List<string> { "Normal", "Flying" }),
            new Pokemon(84, "Doduo", new List<string> { "Normal", "Flying" }),
            new Pokemon(85, "Dodrio", new List<string> { "Normal", "Flying" }),
            new Pokemon(86, "Seel", new List<string> { "Water" }),
            new Pokemon(87, "Dewgong", new List<string> { "Water", "Ice" }),
            new Pokemon(88, "Grimer", new List<string> { "Poison" }),
            new Pokemon(89, "Muk", new List<string> { "Poison" }),
            new Pokemon(90, "Shellder", new List<string> { "Water" }),
            new Pokemon(91, "Cloyster", new List<string> { "Water", "Ice" }),
            new Pokemon(92, "Gastly", new List<string> { "Ghost", "Poison" }),
            new Pokemon(93, "Haunter", new List<string> { "Ghost", "Poison" }),
            new Pokemon(94, "Gengar", new List<string> { "Ghost", "Poison" }),
            new Pokemon(95, "Onix", new List<string> { "Rock", "Ground" }),
            new Pokemon(96, "Drowzee", new List<string> { "Psychic" }),
            new Pokemon(97, "Hypno", new List<string> { "Psychic" }),
            new Pokemon(98, "Krabby", new List<string> { "Water" }),
            new Pokemon(99, "Kingler", new List<string> { "Water" }),
            new Pokemon(100, "Voltorb", new List<string> { "Electric" }),
            new Pokemon(101, "Electrode", new List<string> { "Electric" }),
            new Pokemon(102, "Exeggcute", new List<string> { "Grass", "Psychic" }),
            new Pokemon(103, "Exeggutor", new List<string> { "Grass", "Psychic" }),
            new Pokemon(104, "Cubone", new List<string> { "Ground" }),
            new Pokemon(105, "Marowak", new List<string> { "Ground" }),
            new Pokemon(106, "Hitmonlee", new List<string> { "Fighting" }),
            new Pokemon(107, "Hitmonchan", new List<string> { "Fighting" }),
            new Pokemon(108, "Lickitung", new List<string> { "Normal" }),
            new Pokemon(109, "Koffing", new List<string> { "Poison" }),
            new Pokemon(110, "Weezing", new List<string> { "Poison" }),
            new Pokemon(111, "Rhyhorn", new List<string> { "Ground", "Rock" }),
            new Pokemon(112, "Rhydon", new List<string> { "Ground", "Rock" }),
            new Pokemon(113, "Chansey", new List<string> { "Normal" }),
            new Pokemon(114, "Tangela", new List<string> { "Grass" }),
            new Pokemon(115, "Kangaskhan", new List<string> { "Normal" }),
            new Pokemon(116, "Horsea", new List<string> { "Water" }),
            new Pokemon(117, "Seadra", new List<string> { "Water" }),
            new Pokemon(118, "Goldeen", new List<string> { "Water" }),
            new Pokemon(119, "Seaking", new List<string> { "Water" }),
            new Pokemon(120, "Staryu", new List<string> { "Water" }),
            new Pokemon(121, "Starmie", new List<string> { "Water", "Psychic" }),
            new Pokemon(122, "Mr. Mime", new List<string> { "Psychic", "Fairy" }),
            new Pokemon(123, "Scyther", new List<string> { "Bug", "Flying" }),
            new Pokemon(124, "Jynx", new List<string> { "Ice", "Psychic" }),
            new Pokemon(125, "Electabuzz", new List<string> { "Electric" }),
            new Pokemon(126, "Magmar", new List<string> { "Fire" }),
            new Pokemon(127, "Pinsir", new List<string> { "Bug" }),
            new Pokemon(128, "Tauros", new List<string> { "Normal" }),
            new Pokemon(129, "Magikarp", new List<string> { "Water" }),
            new Pokemon(130, "Gyarados", new List<string> { "Water", "Flying" }),
            new Pokemon(131, "Lapras", new List<string> { "Water", "Ice" }),
            new Pokemon(132, "Ditto", new List<string> { "Normal" }),
            new Pokemon(133, "Eevee", new List<string> { "Normal" }),
            new Pokemon(134, "Vaporeon", new List<string> { "Water" }),
            new Pokemon(135, "Jolteon", new List<string> { "Electric" }),
            new Pokemon(136, "Flareon", new List<string> { "Fire" }),
            new Pokemon(137, "Porygon", new List<string> { "Normal" }),
            new Pokemon(138, "Omanyte", new List<string> { "Rock", "Water" }),
            new Pokemon(139, "Omastar", new List<string> { "Rock", "Water" }),
            new Pokemon(140, "Kabuto", new List<string> { "Rock", "Water" }),
            new Pokemon(141, "Kabutops", new List<string> { "Rock", "Water" }),
            new Pokemon(142, "Aerodactyl", new List<string> { "Rock", "Flying" }),
            new Pokemon(143, "Snorlax", new List<string> { "Normal" }),
            new Pokemon(144, "Articuno", new List<string> { "Ice", "Flying" }),
            new Pokemon(145, "Zapdos", new List<string> { "Electric", "Flying" }),
            new Pokemon(146, "Moltres", new List<string> { "Fire", "Flying" }),
            new Pokemon(147, "Dratini", new List<string> { "Dragon" }),
            new Pokemon(148, "Dragonair", new List<string> { "Dragon" }),
            new Pokemon(149, "Dragonite", new List<string> { "Dragon", "Flying" }),
            new Pokemon(150, "Mewtwo", new List<string> { "Psychic" }),
            new Pokemon(151, "Mew", new List<string> { "Psychic" }),
        };

    }


}
