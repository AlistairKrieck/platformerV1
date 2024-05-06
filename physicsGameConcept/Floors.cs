using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace physicsGameConcept
{
    internal class Floor
    {
        List<Rectangle> platforms = new List<Rectangle>();
        List<Rectangle> boosts = new List<Rectangle>();
        List<Rectangle> npcs = new List<Rectangle>();
        string npcDialog;
        bool checkpoint;

        public Floor(List<Rectangle> platforms, List<Rectangle> boosts, List<Rectangle> npc, bool isCheckpoint, string npcDialog)
        {
            checkpoint = isCheckpoint;
            this.npcDialog = npcDialog;
        }


        public List<Rectangle> Platforms()
        {
            return platforms;
        }

        public List<Rectangle> Boosts()
        {
            return boosts;
        }

        public List<Rectangle> NPCs()
        {
            return npcs;
        }

        public bool Checkpoint()
        {
            return checkpoint;
        }

        public string NPCDialog()
        {
            return npcDialog;
        }
    }
}
