using System.Text;
using System.Threading.Tasks;

namespace SoftUni_SpaceBattle
{

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(5);

            game.Play();
        }
    }
}
