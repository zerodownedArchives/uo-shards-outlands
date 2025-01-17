using System;
using System.Collections.Generic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Custom;

namespace Server.Spells.Fourth
{
	public class ArchCureSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Arch Cure", "Vas An Nox",
				215,
				9061,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot
			);

		public override SpellCircle Circle { get { return SpellCircle.Fourth; } }

		public ArchCureSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
            BaseCreature casterCreature = Caster as BaseCreature;

            if (casterCreature != null)
            {
                if (casterCreature.SpellTarget != null)
                {
                    this.Target(casterCreature.SpellTarget);
                }
            }

            else
            {
                Caster.Target = new InternalTarget(this);
            }
		}
        
		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}

			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 3 );

					foreach ( Mobile m in eable )
					{
						if ( Caster.CanBeBeneficial( m, false ) )
							targets.Add( m );
					}

					eable.Free();
				}

				Effects.PlaySound( p, Caster.Map, 0x299 );

				if ( targets.Count > 0 )
				{
					int cured = 0;

                    int spellHue = Enhancements.GetMobileSpellHue(Caster, Enhancements.SpellType.ArchCure);      

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile mobile = targets[i];

						if( mobile.Player )
							Caster.DoBeneficial( mobile );

						if ( mobile.CurePoison( Caster ) )
							++cured;

                        if (mobile.Poisoned)
                        {
                            AspectArmorProfile aspectArmorProfile = AspectGear.GetAspectArmorProfile(mobile);

                            //Poison Aspect
                            if (aspectArmorProfile != null)
                            {
                                if (aspectArmorProfile.m_Aspect == AspectEnum.Poison)
                                {
                                    double extraCureChance = AspectGear.PoisonCureChanceBonus * (AspectGear.PoisonCureChanceBonusPerTier * (double)aspectArmorProfile.m_TierLevel);

                                    if (Utility.RandomDouble() <= extraCureChance)
                                    {
                                        ++cured;

                                        mobile.CurePoison(Caster);

                                        //TEST: Add Aspect Visuals
                                    }
                                }
                            } 
                        }

                        mobile.FixedParticles(0x373A, 10, 15, 5012, spellHue, 0, EffectLayer.Waist);
						mobile.PlaySound( 0x1E0 );
					}

					if ( cured > 0 )
						Caster.SendLocalizedMessage( 1010058 );
				}
			}

			FinishSequence();
		}

		private bool IsAggressor( Mobile m )
		{
			foreach ( AggressorInfo info in Caster.Aggressors )
			{
				if ( m == info.Attacker && !info.Expired )
					return true;
			}

			return false;
		}

		private bool IsAggressed( Mobile m )
		{
			foreach ( AggressorInfo info in Caster.Aggressed )
			{
				if ( m == info.Defender && !info.Expired )
					return true;
			}

			return false;
		}

		private class InternalTarget : Target
		{
			private ArchCureSpell m_Owner;

			public InternalTarget( ArchCureSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}