using System;

namespace lo_novo.LabRaid
{
    public class WestMaintenance : Room
    {
        // Dwarr: go into options and pls change tab settings to USE SPACES, width 4 SPACES
        // change right-column marker to 120 if you're OCD

        // from now on i will communicate exclusively via comments.

        // done // good shit!
        
        #region implemented abstract members of Room

        public override string Name { get { return "Maintenance Cupboard (W)"; } }

        public override string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public WestMaintenance()
        {
            // so... what's in this maintenance board for cups?
            
            // ConceptThing is a dumb ass thing that just spits strings back
            /*        public ConceptThing(string name, 
            string furtherInRoomDescription = null, string activate = null, string attack = null,
            string pushPull = null, string talk = null, string take = null, string punt = null,
            string stop = null, string openClose = null, string climbDescend = null, string modify = null)*/
            // What other types of Thing are there, again?
            // literally the only type in the codebase right now is SalesmanControlRoomThing, a custom npc
            // lol
            // yeah. i'm thinking ConceptThing should learn to take functions as well as strings, and become the new base Thing class.
            // sounds good cool cool.
            
            var t1 = new ConceptThing("low ceiling", "An exceedingly low ceiling, for such a spacious spacious-ship.",
                push="Raise the roof!"));
            t1.Important = false;
            Things.Add(t1);

            ConceptThing oxy;
            oxy = new ConceptThing("oxygen supply control", 
                @"This looks like it controls the oxygen supply for the entire ship! It's currently at 100%.
The dial seems far too tempting.", 
                take="You'd take away all oxygen on ship?! You monster!", // or "That's just greedy." <- mebbeh too confusing tho
                activate=(i) => {
                    // Is this a turny dial? 0% to 100%? can space age tech really boil down to JUST THIS (maybe it can) < definitely. and make it get stuck, not even a good turny dial.
                    // hahahaahahha!!!!!!!! yes. i agree.
                    // adding int field 0..100 .Oxygen  to static class LabRaidState         public static int Oxygen = 100;
                    if (LabRaidState.Oxygen > 0) // gets stuck OFF
                    {
                        // Oxygen % ALWAYS lowers.
                        // Need some custom actions/custom Thing code so if they try and turn it back we acknowledge this...??? Yeah! "too late..."

                        State.o("You twist the crusty dial, and the oxygen level lowers.");

                        if (LabRaidState.Oxygen == 100)
                        {
                            State.Ticking.Add(new SimpleTick(() => {
                                // BEGIN DEATH TIMER.
                                
                            }));
                        }
                        
                        LabRaidState.Oxygen -= (State.RNG.Next() > 0.5 ? 20 : 30);
                        LabRaidState.Oxygen = (LabRaidState.Oxygen < 0 ? 0 : LabRaidState.Oxygen);

                        if (LabRaidState.Oxygen < 20) { ... }
                        else if (LabRaidState.Oxygen < 50) { ... }
                        else if (LabRaidState.Oxygen < 81) { ... }
                    }
                    else
                        State.o("No good. It's firmly stuck at 0%.\nIs it just me, or is it a bit stuffy in here?");
                    // TODO: UPDATE OBJECT DESC BASED ON OXY %. And possibly a bunch of responses when it gets desperate.
                },
                pushPull="It's a turny knob, not a pushy-pulley.",
                talk="I talk to the wind. But the wind cannot hear...",
                punt="That's a worrying noise...", // hahaha!
            );
            Things.Add(oxy);

            // NOT added unless party fucks up the cables
            var fireAdded = false;
            var fire = new ConceptThing("small but growing fire",
            @"A worrying fire near the power controls. Is this what they call an electrical fire?
It's spreading slowly towards the safe.",
            // STUFF HERE
            );


            var redWire = new Wire("red");
            var blueWire = new Wire("blue");
            var purpleWire = new Wire("purple");
            var mauveWire = new Wire("mauve");
            var burgundyWire = new Wire("burgundy");
            Things.AddRange(new Wire[] { redWire, blueWire, purpleWire, mauveWire, burgundyWire });

            var cables = new ConceptThing("electricity cables",
            @"Wires, frayed and rusted, run from the ceiling to the floor. Red, blue, purple and mauve.
What's a humble space explorer to do?",
            take=@"Yeah, of course, take the LIVE WIRES. That'll end well.
The only thing you're taking, sir, is the piss!",
            punt=(i) => { // arg passed in will be Intention; the parsing result object
                if (State.RNG.Next() > 0.2)
                {
                    // spark produced -- if high oxygen, this creates a fireball
                    //State.Player.Eyebrows = false;
                }
                else
                {
                    // fire produced if oxygen available
                    // fire might actually be useful! k.
                    if (LabRaidState.Oxygen > 20 && !fireAdded)
                        if (i.Violence > 3)
                        {
                            State.o("You violently " + i.VerbString + " the cabling, and much sparking occurs.");
                            
                        }
                        else
                            State.o("You pathetically " + i.VerbString + " the cables. They rattle, but they aren't. Uh, rattled.");
                    else if (fireAdded)
                        State.o("Another electrical fire completely fails to occur. Thank Sol the oxygen is so low!");
                    else
                        State.o("Some embers break over the wall, but the lack of oxygen means nothing ignites.");
                }
            },
            activate=(i) => { // for if they're trying to reroute the wires to achieve something?
            
                // XXX
                // XXX: TODO: BREAK THIS OUT INTO ROOM SCRIPT. At this point, ActiveNoun must be the cables as a *whole*.
                // XXX

                if (i.ActiveNoun == 
                
                if ((i.ActiveNoun as Wire).Cut)
                {
                    State.o("Alas, this wire is already cut."); // oh, good catch.
                    return true;
                }
                State.o("This is bound to end well! What a clever idea!");
                State.o("RED WIRE or BLUE WIRE?");
                // creation of Wire instances ^^^^^^. redWire, blueWire, mauveWire, purpleWire, burgundyWire
                if (i.PassiveNoun is Pliers)
                {
                    State.o("Smart thinking. That'll keep the electricity out of your insides."); // haahahahaahah
                    if (i.ActiveNoun == redWire)
                    {
                        State.o(@"You honestly cut the RED WIRE? Wow.
You hear the sound of 53 doors and one viewing window all locking in unison."); // hahahahaahahahaha
                        // TODO: add convenience func, deal with case where only 1 player
                        State.o("<" + State.AllPlayers.Except(State.Player).ChooseRandom().Name + "> " + State.Player.Name + ", you're a fucking moron.");
                        // hahahahaha
                        //State.Player.Personality.Oblivious++;
                        //PLAYER KILL SELF WITH PETRIL/CHEESE
                        LabRaidState.DoorsLocked = true; // every door must now be broken to open? hahahahahaha.
                        // ^ to support this, should break Exits code out of parser & hack up Exits code. doable though.
                        // should automatically inject un-Important-when-unlocked Door objects into rooms, too...
                        
                        // maybe too cruel // probably. want to assauge your guilt? it's easy! use the RNG.
                        // set % to how sure you are it's too cruel. or 100-that percent. i forget.
                        // hahaha
                        // actually, nah. It's the RED wire - come on! They deserve it.
                    }

                    else if (i.ActiveNoun == blueWire)
                    {
                        State.o("Well, that was a fairly obvious choice.");
                        LabRaidState.SecurityActive = true; // maybe?
                    }
                }
                else if (i.PassiveNoun is Player)
                {
                    if (i.ActiveNoun == redWire)
                        if (LabRaidState.SecurityActive)
                        {
                            State.o((i.PassiveNoun as Player).Name + " agrees, and disconnects the RED WIRE with ease. That went really well!");
                            LabRaidState.SecurityActive = false;
                        }
                        else
                            State.o("They smugly decline to give you a repeat performance.");
                    else
                        Adlib.Refuses(i.PassiveNoun);
                }

                else // TODO: perhaps use other tools than just pliers
                {
                    State.o("YOUCH! Your fingers are all burned and crispy now. And the RED WIRE isn't affected at all.");
                }

                // maybe a clue you can find somewhere else? or a puzzle?
                State.o("MAUVE WIRE or PURPLE WIRE?"); // < troll i get it. DARK RED or BURGUNDY? :D burgeringdjjiy. burger king. burgherndy. 
                if (i.PassiveNoun is Pliers)
                {

                    State.o("Pliers! Like fingers, but not conductive across the user's chest cavity.");
                    if (i.ActiveNoun == mauveWire)
                    {
                        State.o(@"Mauve is *so* last season.
What did cutting this do?"); // maybe this chooses between two prizes later in the level? easily achieved!
                        LabRaidState.MauveSyndrome = true;

                    }
                    else if (i.ActiveNoun == purpleWire)
                    {
                        State.o("Purple is *in* this season."); // hah!
                        //State.Player.Personality.FashionSense++; < hahaha.
                        // with regards to committing/saving this; i'm thinking of just copying all changes into my git wholesale,
                        //  and pushing to remote. it won't compile; who cares. either comment out or fix. 
                        // it'll help motivate me to do the latter.
                        // Yeah, good point. Sounds like a plan.
                        // we are men with plen, aplombaly. are we done, or is there something you want to finish?
                        // oh; and let's do this again. when do you next think you may be able to spare this 2-3 hours?
                        // (preferably +>=4 days from today, so i have time to write basic code we relied on here
                        //   e.g. actually useful conceptthing)
                        // Maybe next weekend? Maybe even in the week, but I don't know when.
                        // k. well i'm not incredibly busy atm. i'll make you do it next weekend if you're not ready before,
                        //  but frankly just grabbing me when the opportunity appears is probably the best bet.
                        // Come on. This was fun, and productive! Bouncing off ideas is greeeaaaaat.
                        // Yeah, yeah! I liked it. I just don't know how much stuff I've got to do in the week.
                        // As you ramp up into employment, it seems I'll be doing the reverse.
                        // It'll give me time to write support stuff anyways.
                        // Anyway; done done?
                        // DUN DUN DUUUUUUN!!!.
                        if (!LabRaidState.SecurityActive)
                            State.o(@"Those lights you extinguished when you cut the other wires? They're blink on..
A series of clanks and gutteral noises demonstrate the system's reinvigoration, vigorously."); // hahaha last sentence is awks but i can't think of proper words? nor can I, really this'll do.
                            // tbh i'm running out of steam a bit. we did good tonight..
                            // yeah, steam running low
                            // fun, though, no? you like the collaborative thing?
                            // yeah! this is great for constriction. of the mind flowing word vessel things. ligaments.
                            // yep; went better than i expected, actually.
                            // re. blue wire; we should do something else for that. if only just a gag. yeah, we should, shouldn't we? mm
                            
                        else
                            State.o("Strange. Nothing happened.");
                        LabRaidState.SecurityActive = true; // don't push your luck, player
                        
                    }
                }

                else // TODO: perhaps use other tools than just pliers
                {
                    State.o("YOUCH! Your fingers are all burned and crispy now. And the RED WIRE isn't affected at all.");
                }


            },
            talk="Like a bird on the wire... better stop talking to the wire." // ha.
            );
            Things.Add(cables);


            // when you have code for managing Things, everything looks like a Thing. likewise for lambdas...

            Things.Add(new ConceptThing("extremely useful looking safe", // but it's LOCKED! OMGZ.
            ));


            Things.Add(new ConceptThing("humour gas controls", // haha
            ));
            AddExit(Direction.East, typeof(WestCorridorS), "corridor");
        }
    }
}

