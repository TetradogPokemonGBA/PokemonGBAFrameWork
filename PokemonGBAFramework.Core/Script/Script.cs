/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:08
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.BuildScript;
using PokemonGBAFramework.Core.ComandosScript;
using PokemonGBAFramework.Core.Extension;

namespace PokemonGBAFramework.Core
{

    /// <summary>
    /// Description of Script.
    /// </summary>
    public class Script : IDeclaracion, IBloqueConNombre, ILastResult, IEndScript,IComparable<Script>
    {

        const string ENTER = "\r\n";   //enter con el formato correcto en windows// \r\n
        public const byte ID = 0xF;
        public const int SINERROR = -1;
     
        public const byte RETURN = 0x3;
        public const byte END = 0x2;

        public static readonly Creditos Creditos;
        static GenIdInt GenId { get; set; }


        

        public static Hex OffsetInicioDynamic = "800000";

        string nombreBloque;

        static Script()
        {
            GenId = new GenIdInt(1);
            Creditos = new Creditos();
            Creditos.Add("XSE", "HackMew", "Hacer la aplicacion y sus explicaciones");

        }


        #region Constructores y Cargar
        public Script()
        {
            ComandosScript = new Llista<Comando>();
            IdUnicoTemp = GetIdUnicoTemp();
        }



        #region Leer Compilado
        public Script(RomGba rom, OffsetRom offsetScript):this(new ScriptAndASMManager(), rom, offsetScript) { }
        public Script(ScriptAndASMManager scriptManager, RomGba rom, OffsetRom offsetScript) : this(scriptManager,rom, offsetScript.Offset) { }
        public Script(RomGba rom, int offsetScript):this(new ScriptAndASMManager(),rom,offsetScript) { }
        public Script(ScriptAndASMManager scriptManager, RomGba rom, int offsetScript)
            : this(scriptManager,rom.Data.Bytes, offsetScript) { }
        public Script(byte[] bytesScript, int offset = 0) : this(new ScriptAndASMManager(),bytesScript, offset) { }
        public Script(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset = 0)
            : this()
        {
            unsafe
            {
                fixed (byte* ptBytesScirpt = bytesScript)
                    Cargar(scriptManager,ptBytesScirpt, offset);

            }

        }
        public unsafe Script( byte* ptRom, int offsetScript):this(new ScriptAndASMManager(),ptRom,offsetScript) { }
        public unsafe Script(ScriptAndASMManager scriptManager, byte* ptRom, int offsetScript)
            : this()
        {
            Cargar(scriptManager,ptRom, offsetScript);
        }
        unsafe int Cargar(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetScript,bool excepcionOCodigoError=true)
        {
            //quizas no siempre acaba en end o return y acaba por ejemplo llamando a otra funcion...por testear la solución...
            //obtengo los comandos hasta encontrar return , end o un comando que acabe la función
            //podria ser que un byteComandoActual depende de una edicion u otra llama a una funcion...u otra???sino no me lo explico...
            
            byte byteComandoActual;
            Comando comandoActual;
            int inicio = offsetScript;
            int codigoError= SINERROR;
            IEndScript endScriptComando = null;
            do
            {
                comandoActual = null;
                byteComandoActual = ptrRom[offsetScript];
                offsetScript++;
                switch (byteComandoActual)
                {
                    //pongo los comandos
                    case Nop.ID:
                        comandoActual = new Nop(scriptManager,ptrRom, offsetScript);
                        break;
                    case Nop1.ID:
                        comandoActual = new Nop1(scriptManager,ptrRom, offsetScript);
                        break;
                    case Call.ID:
                        comandoActual = new Call(scriptManager,ptrRom, offsetScript);
                        break;
                    case Goto.ID:
                        comandoActual = new Goto(scriptManager,ptrRom, offsetScript);
                        break;
                    case If1.ID:
                        comandoActual = new If1(scriptManager,ptrRom, offsetScript);
                        break;
                    case If2.ID:
                        comandoActual = new If2(scriptManager,ptrRom, offsetScript);
                        break;
                    case Gotostd.ID:
                        comandoActual = new Gotostd(scriptManager,ptrRom, offsetScript);
                        break;
                    case Callstd.ID:
                        comandoActual = new Callstd(scriptManager,ptrRom, offsetScript);
                        break;
                    case Gotostdif.ID:
                        comandoActual = new Gotostdif(scriptManager,ptrRom, offsetScript);
                        break;
                    case Callstdif.ID:
                        comandoActual = new Callstdif(scriptManager,ptrRom, offsetScript);
                        break;
                    case Jumpram.ID:
                        comandoActual = new Jumpram(scriptManager,ptrRom, offsetScript);
                        break;
                    case Killscript.ID:
                        comandoActual = new Killscript(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetByte.ID:
                        comandoActual = new SetByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case LoadPointer.ID:
                        comandoActual = new LoadPointer(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetByte2.ID:
                        comandoActual = new SetByte2(scriptManager,ptrRom, offsetScript);
                        break;
                    case WriteByteToOffset.ID:
                        comandoActual = new WriteByteToOffset(scriptManager,ptrRom, offsetScript);
                        break;
                    case LoadByteFromPointer.ID:
                        comandoActual = new LoadByteFromPointer(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetFarByte.ID:
                        comandoActual = new SetFarByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case CopyScriptBanks.ID:
                        comandoActual = new CopyScriptBanks(scriptManager,ptrRom, offsetScript);
                        break;
                    case CopyByte.ID:
                        comandoActual = new CopyByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetVar.ID:
                        comandoActual = new SetVar(scriptManager,ptrRom, offsetScript);
                        break;
                    case AddVar.ID:
                        comandoActual = new AddVar(scriptManager,ptrRom, offsetScript);
                        break;
                    case SubVar.ID:
                        comandoActual = new SubVar(scriptManager,ptrRom, offsetScript);
                        break;
                    case CopyVar.ID:
                        comandoActual = new CopyVar(scriptManager,ptrRom, offsetScript);
                        break;
                    case CopyVarIfNotZero.ID:
                        comandoActual = new CopyVarIfNotZero(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareBanks.ID:
                        comandoActual = new CompareBanks(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareBankToByte.ID:
                        comandoActual = new CompareBankToByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareBankToFarByte.ID:
                        comandoActual = new CompareBankToFarByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareFarByteToBank.ID:
                        comandoActual = new CompareBankToFarByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareFarByteToByte.ID:
                        comandoActual = new CompareFarByteToByte(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareFarBytes.ID:
                        comandoActual = new CompareFarBytes(scriptManager,ptrRom, offsetScript);
                        break;
                    case Compare.ID:
                        comandoActual = new Compare(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareVars.ID:
                        comandoActual = new CompareVars(scriptManager,ptrRom, offsetScript);
                        break;
                    case CallAsm.ID:
                        comandoActual = new CallAsm(scriptManager,ptrRom, offsetScript);
                        break;
                    case Cmd24.ID:
                        comandoActual = new Cmd24(scriptManager,ptrRom, offsetScript);
                        break;
                    case Special.ID:
                        comandoActual = new Special(scriptManager,ptrRom, offsetScript);
                        break;
                    case Special2.ID:
                        comandoActual = new Special2(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitState.ID:
                        comandoActual = new WaitState(scriptManager,ptrRom, offsetScript);
                        break;
                    case Pause.ID:
                        comandoActual = new Pause(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetFlag.ID:
                        comandoActual = new SetFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case ClearFlag.ID:
                        comandoActual = new ClearFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckFlag.ID:
                        comandoActual = new CheckFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case Cmd2C.ID:
                        comandoActual = new Cmd2C(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckSound.ID:
                        comandoActual = new CheckSound(scriptManager,ptrRom, offsetScript);
                        break;
                    case FanFare.ID:
                        comandoActual = new FanFare(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitFanFare.ID:
                        comandoActual = new WaitFanFare(scriptManager,ptrRom, offsetScript);
                        break;
                    case PlaySong.ID:
                        comandoActual = new PlaySong(scriptManager,ptrRom, offsetScript);
                        break;
                    case PlaySong2.ID:
                        comandoActual = new PlaySong2(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeDefault.ID:
                        comandoActual = new FadeDefault(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeSong.ID:
                        comandoActual = new FadeSong(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeOut.ID:
                        comandoActual = new FadeOut(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeIn.ID:
                        comandoActual = new FadeIn(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckDailyFlags.ID:
                        comandoActual = new CheckDailyFlags(scriptManager,ptrRom, offsetScript);
                        break;
                    case ResetVars.ID:
                        comandoActual = new ResetVars(scriptManager,ptrRom, offsetScript);
                        break;
                    case Sound.ID:
                        comandoActual = new Sound(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp.ID:
                        comandoActual = new Warp(scriptManager,ptrRom, offsetScript);
                        break;
                    case WarpMuted.ID:
                        comandoActual = new WarpMuted(scriptManager,ptrRom, offsetScript);
                        break;
                    case WarpWalk.ID:
                        comandoActual = new WarpWalk(scriptManager,ptrRom, offsetScript);
                        break;
                    case WarpHole.ID:
                        comandoActual = new WarpHole(scriptManager,ptrRom, offsetScript);
                        break;
                    case WarpTeleport.ID:
                        comandoActual = new WarpTeleport(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp3.ID:
                        comandoActual = new Warp3(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetWarpplace.ID:
                        comandoActual = new SetWarpplace(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp4.ID:
                        comandoActual = new Warp4(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp5.ID:
                        comandoActual = new Warp5(scriptManager,ptrRom, offsetScript);
                        break;
                    case GetPlayerPos.ID:
                        comandoActual = new GetPlayerPos(scriptManager,ptrRom, offsetScript);
                        break;
                    case CountPokemon.ID:
                        comandoActual = new CountPokemon(scriptManager,ptrRom, offsetScript);
                        break;
                    case AddItem.ID:
                        comandoActual = new AddItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case RemoveItem.ID:
                        comandoActual = new RemoveItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckItemRoom.ID:
                        comandoActual = new CheckItemRoom(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckItem.ID:
                        comandoActual = new CheckItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckItemType.ID:
                        comandoActual = new CheckItemType(scriptManager,ptrRom, offsetScript);
                        break;
                    case AddPcItem.ID:
                        comandoActual = new AddPcItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckPcItem.ID:
                        comandoActual = new CheckPcItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case AddDecoration.ID:
                        comandoActual = new AddDecoration(scriptManager,ptrRom, offsetScript);
                        break;
                    case RemoveDecoration.ID:
                        comandoActual = new RemoveDecoration(scriptManager,ptrRom, offsetScript);
                        break;
                    case TestDecoration.ID:
                        comandoActual = new TestDecoration(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckDecoration.ID:
                        comandoActual = new CheckDecoration(scriptManager,ptrRom, offsetScript);
                        break;
                    case ApplyMovement.ID:
                        comandoActual = new ApplyMovement(scriptManager,ptrRom, offsetScript);
                        break;
                    case ApplyMovementPos.ID:
                        comandoActual = new ApplyMovementPos(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitMovement.ID:
                        comandoActual = new WaitMovement(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitMovementPos.ID:
                        comandoActual = new WaitMovementPos(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideSprite.ID:
                        comandoActual = new HideSprite(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideSpritePos.ID:
                        comandoActual = new HideSpritePos(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowSprite.ID:
                        comandoActual = new ShowSprite(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowSpritePos.ID:
                        comandoActual = new ShowSpritePos(scriptManager,ptrRom, offsetScript);
                        break;
                    case MoveSprite.ID:
                        comandoActual = new MoveSprite(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteVisible.ID:
                        comandoActual = new SpriteVisible(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteInvisible.ID:
                        comandoActual = new SpriteInvisible(scriptManager,ptrRom, offsetScript);
                        break;
                    case Faceplayer.ID:
                        comandoActual = new Faceplayer(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteFace.ID:
                        comandoActual = new SpriteFace(scriptManager,ptrRom, offsetScript);
                        break;
                    case Trainerbattle.ID:
                        comandoActual = new Trainerbattle(scriptManager,ptrRom, offsetScript);
                        break;
                    case RepeatTrainerBattle.ID:
                        comandoActual = new RepeatTrainerBattle(scriptManager,ptrRom, offsetScript);
                        break;
                    case EndTrainerBattle.ID:
                        comandoActual = new EndTrainerBattle(scriptManager,ptrRom, offsetScript);
                        break;
                    case EndTrainerBattle2.ID:
                        comandoActual = new EndTrainerBattle2(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckTrainerFlag.ID:
                        comandoActual = new CheckTrainerFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case ClearTrainerFlag.ID:
                        comandoActual = new ClearTrainerFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetTrainerFlag.ID:
                        comandoActual = new SetTrainerFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case MoveSprite2.ID:
                        comandoActual = new MoveSprite2(scriptManager,ptrRom, offsetScript);
                        break;
                    case MoveOffScreen.ID:
                        comandoActual = new MoveOffScreen(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteBehave.ID:
                        comandoActual = new SpriteBehave(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitMsg.ID:
                        comandoActual = new WaitMsg(scriptManager,ptrRom, offsetScript);
                        break;
                    case PrepareMsg.ID:
                        comandoActual = new PrepareMsg(scriptManager,ptrRom, offsetScript);
                        break;
                    case CloseOnKeyPress.ID:
                        comandoActual = new CloseOnKeyPress(scriptManager,ptrRom, offsetScript);
                        break;
                    case LockAll.ID:
                        comandoActual = new LockAll(scriptManager,ptrRom, offsetScript);
                        break;
                    case Lock.ID:
                        comandoActual = new Lock(scriptManager,ptrRom, offsetScript);
                        break;
                    case ReleaseAll.ID:
                        comandoActual = new ReleaseAll(scriptManager,ptrRom, offsetScript);
                        break;
                    case Release.ID:
                        comandoActual = new Release(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitKeyPress.ID:
                        comandoActual = new WaitKeyPress(scriptManager,ptrRom, offsetScript);
                        break;
                    case YesNoBox.ID:
                        comandoActual = new YesNoBox(scriptManager,ptrRom, offsetScript);
                        break;
                    case Multichoice.ID:
                        comandoActual = new Multichoice(scriptManager,ptrRom, offsetScript);
                        break;
                    case Multichoice2.ID:
                        comandoActual = new Multichoice2(scriptManager,ptrRom, offsetScript);
                        break;
                    case Multichoice3.ID:
                        comandoActual = new Multichoice3(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowBox.ID:
                        comandoActual = new ShowBox(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideBox.ID:
                        comandoActual = new HideBox(scriptManager,ptrRom, offsetScript);
                        break;
                    case ClearBox.ID:
                        comandoActual = new ClearBox(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowPokePic.ID:
                        comandoActual = new ShowPokePic(scriptManager,ptrRom, offsetScript);
                        break;
                    case HidePokePic.ID:
                        comandoActual = new HidePokePic(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowContestWinner.ID:
                        comandoActual = new ShowContestWinner(scriptManager,ptrRom, offsetScript);
                        break;
                    case Braille.ID:
                        comandoActual = new Braille(scriptManager,ptrRom, offsetScript);
                        break;
                    case GivePokemon.ID:
                        comandoActual = new GivePokemon(scriptManager,ptrRom, offsetScript);
                        break;
                    case GiveEgg.ID:
                        comandoActual = new GiveEgg(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetPokemonPP.ID:
                        comandoActual = new SetPokemonPP(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckAttack.ID:
                        comandoActual = new CheckAttack(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferPokemon.ID:
                        comandoActual = new BufferPokemon(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferFirstPokemon.ID:
                        comandoActual = new BufferFirstPokemon(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferPartyPokemon.ID:
                        comandoActual = new BufferPartyPokemon(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferItem.ID:
                        comandoActual = new BufferItem(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferDecoration.ID:
                        comandoActual = new BufferDecoration(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferAttack.ID:
                        comandoActual = new BufferAttack(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferNumber.ID:
                        comandoActual = new BufferNumber(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferStd.ID:
                        comandoActual = new BufferStd(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferString.ID:
                        comandoActual = new BufferString(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokeMart.ID:
                        comandoActual = new PokeMart(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokeMart2.ID:
                        comandoActual = new PokeMart2(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokeMart3.ID:
                        comandoActual = new PokeMart3(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokeCasino.ID:
                        comandoActual = new PokeCasino(scriptManager,ptrRom, offsetScript);
                        break;
                    case Cmd8A.ID:
                        comandoActual = new Cmd8A(scriptManager,ptrRom, offsetScript);
                        break;
                    case ChooseContestPkmn.ID:
                        comandoActual = new ChooseContestPkmn(scriptManager,ptrRom, offsetScript);
                        break;
                    case StartContest.ID:
                        comandoActual = new StartContest(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowContestResults.ID:
                        comandoActual = new ShowContestResults(scriptManager,ptrRom, offsetScript);
                        break;
                    case ContestLinkTransfer.ID:
                        comandoActual = new ContestLinkTransfer(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokemonGBAFramework.Core.ComandosScript.Random.ID:
                        comandoActual = new PokemonGBAFramework.Core.ComandosScript.Random(scriptManager,ptrRom, offsetScript);
                        break;
                    //estos me los salto
                    //falta añadir asta CRY incluido
                    case GiveMoney.ID:
                        comandoActual = new GiveMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case PayMoney.ID:
                        comandoActual = new PayMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckMoney.ID:
                        comandoActual = new CheckMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowMoney.ID:
                        comandoActual = new ShowMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideMoney.ID:
                        comandoActual = new HideMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case UpdateMoney.ID:
                        comandoActual = new UpdateMoney(scriptManager,ptrRom, offsetScript);
                        break;
                    case Cmd96.ID:
                        comandoActual = new Cmd96(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeScreen.ID:
                        comandoActual = new FadeScreen(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeScreenDelay.ID:
                        comandoActual = new FadeScreenDelay(scriptManager,ptrRom, offsetScript);
                        break;
                    case Darken.ID:
                        comandoActual = new Darken(scriptManager,ptrRom, offsetScript);
                        break;
                    case Lighten.ID:
                        comandoActual = new Lighten(scriptManager,ptrRom, offsetScript);
                        break;
                    case PrepareMsg2.ID:
                        comandoActual = new PrepareMsg2(scriptManager,ptrRom, offsetScript);
                        break;
                    case DoAnimation.ID:
                        comandoActual = new DoAnimation(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetAnimation.ID:
                        comandoActual = new SetAnimation(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckAnimation.ID:
                        comandoActual = new ContestLinkTransfer(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetHealingPlace.ID:
                        comandoActual = new SetHealingPlace(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckGender.ID:
                        comandoActual = new CheckGender(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokemonGBAFramework.Core.ComandosScript.Cry.ID:
                        comandoActual = new PokemonGBAFramework.Core.ComandosScript.Cry(scriptManager,ptrRom, offsetScript);
                        break;

                    case SetMapTile.ID:
                        comandoActual = new SetMapTile(scriptManager,ptrRom, offsetScript);
                        break;
                    case ResetWeather.ID:
                        comandoActual = new ResetWeather(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetWeather.ID:
                        comandoActual = new SetWeather(scriptManager,ptrRom, offsetScript);
                        break;
                    case DoWeather.ID:
                        comandoActual = new DoWeather(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdA6.ID:
                        comandoActual = new CmdA6(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetMapFooter.ID:
                        comandoActual = new SetMapFooter(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteLevelUp.ID:
                        comandoActual = new SpriteLevelUp(scriptManager,ptrRom, offsetScript);
                        break;
                    case RestoreSpriteLevel.ID:
                        comandoActual = new RestoreSpriteLevel(scriptManager,ptrRom, offsetScript);
                        break;
                    case CreateSprite.ID:
                        comandoActual = new CreateSprite(scriptManager,ptrRom, offsetScript);
                        break;
                    case SpriteFace2.ID:
                        comandoActual = new SpriteFace2(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetDoorOpened.ID:
                        comandoActual = new SetDoorOpened(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetDoorClosed.ID:
                        comandoActual = new SetDoorClosed(scriptManager,ptrRom, offsetScript);
                        break;
                    case DoorChange.ID:
                        comandoActual = new DoorChange(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetDoorOpened2.ID:
                        comandoActual = new SetDoorOpened2(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdB1.ID:
                        comandoActual = new CmdB1(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdB2.ID:
                        comandoActual = new CmdB2(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckCoins.ID:
                        comandoActual = new CheckCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case GiveCoins.ID:
                        comandoActual = new GiveCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case RemoveCoins.ID:
                        comandoActual = new RemoveCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetWildBattle.ID:
                        comandoActual = new SetWildBattle(scriptManager,ptrRom, offsetScript);
                        break;
                    case DoWildBattle.ID:
                        comandoActual = new DoWildBattle(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetVirtualAddress.ID:
                        comandoActual = new SetVirtualAddress(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualGoto.ID:
                        comandoActual = new VirtualGoto(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualCall.ID:
                        comandoActual = new VirtualCall(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualGotoIf.ID:
                        comandoActual = new VirtualGotoIf(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualCallIf.ID:
                        comandoActual = new VirtualCallIf(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualMsgBox.ID:
                        comandoActual = new VirtualMsgBox(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualLoadPointer.ID:
                        comandoActual = new VirtualLoadPointer(scriptManager,ptrRom, offsetScript);
                        break;
                    case VirtualBuffer.ID:
                        comandoActual = new VirtualBuffer(scriptManager,ptrRom, offsetScript);
                        break;
                    case ShowCoins.ID:
                        comandoActual = new ShowCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideCoins.ID:
                        comandoActual = new HideCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case UpdateCoins.ID:
                        comandoActual = new UpdateCoins(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdC3.ID:
                        comandoActual = new CmdC3(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp6.ID:
                        comandoActual = new Warp6(scriptManager,ptrRom, offsetScript);
                        break;
                    case WaitCry.ID:
                        comandoActual = new WaitCry(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferBoxName.ID:
                        comandoActual = new BufferBoxName(scriptManager,ptrRom, offsetScript);
                        break;
                    case TextColor.ID:
                        comandoActual = new TextColor(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdC8.ID:
                        comandoActual = new CmdC8(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdC9.ID:
                        comandoActual = new CmdC9(scriptManager,ptrRom, offsetScript);
                        break;
                    case SignMsg.ID:
                        comandoActual = new SignMsg(scriptManager,ptrRom, offsetScript);
                        break;
                    case NormalMsg.ID:
                        comandoActual = new NormalMsg(scriptManager,ptrRom, offsetScript);
                        break;
                    case CompareHiddenVar.ID:
                        comandoActual = new CompareHiddenVar(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetOvedience.ID:
                        comandoActual = new SetOvedience(scriptManager,ptrRom, offsetScript);
                        break;
                    case CheckObedience.ID:
                        comandoActual = new CheckObedience(scriptManager,ptrRom, offsetScript);
                        break;
                    case ExecuteRam.ID:
                        comandoActual = new ExecuteRam(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetWorldMapFlag.ID:
                        comandoActual = new SetWorldMapFlag(scriptManager,ptrRom, offsetScript);
                        break;
                    case WarpTeleport2.ID:
                        comandoActual = new WarpTeleport2(scriptManager,ptrRom, offsetScript);
                        break;
                    case SetCatchLocation.ID:
                        comandoActual = new SetCatchLocation(scriptManager,ptrRom, offsetScript);
                        break;
                    case Braille2.ID:
                        comandoActual = new Braille2(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferItems.ID:
                        comandoActual = new BufferItems(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdD5.ID:
                        comandoActual = new CmdD5(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdD6.ID:
                        comandoActual = new CmdD6(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp7.ID:
                        comandoActual = new Warp7(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdD8.ID:
                        comandoActual = new CmdD8(scriptManager,ptrRom, offsetScript);
                        break;
                    case CmdD9.ID:
                        comandoActual = new CmdD9(scriptManager,ptrRom, offsetScript);
                        break;
                    case HideBox2.ID:
                        comandoActual = new HideBox2(scriptManager,ptrRom, offsetScript);
                        break;
                    case PrepareMsg3.ID:
                        comandoActual = new PrepareMsg3(scriptManager,ptrRom, offsetScript);
                        break;
                    case FadeScreen3.ID:
                        comandoActual = new FadeScreen3(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferTrainerClass.ID:
                        comandoActual = new BufferTrainerClass(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferTrainerName.ID:
                        comandoActual = new BufferTrainerName(scriptManager,ptrRom, offsetScript);
                        break;
                    case PokenavCall.ID:
                        comandoActual = new PokenavCall(scriptManager,ptrRom, offsetScript);
                        break;
                    case Warp8.ID:
                        comandoActual = new Warp8(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferContestType.ID:
                        comandoActual = new BufferContestType(scriptManager,ptrRom, offsetScript);
                        break;
                    case BufferItems2.ID:
                        comandoActual = new BufferItems2(scriptManager,ptrRom, offsetScript);
                        break;
                    case RETURN:
                    case END:
                        break;
                    //si no esta hago una excepcion
                    default:
                        if (excepcionOCodigoError)
                            throw new ScriptRomMalFormadoException(inicio, offsetScript);
                        else codigoError = offsetScript;
                        break;
                }

                if (comandoActual != null)
                {
                    endScriptComando = comandoActual as IEndScript;
                    ComandosScript.Add(comandoActual);
                    offsetScript += comandoActual.ParamsSize;
                }

            } while(codigoError==SINERROR &&!(byteComandoActual == END || byteComandoActual == RETURN || endScriptComando != null && !endScriptComando.IsEnd));
            //tiene que ser un campo calculado...que lea el script y luego devuelva el valor...
            IsEndFinished = endScriptComando == null ? (byteComandoActual == END) : new Nullable<bool>();//si acaba con un goto/call/comandoIEndScript será null si acaba en end será true y si es return pues false
            return codigoError;
        }
        #endregion

        #endregion

        public int IdUnicoTemp { get; private set; }
        public Llista<Comando> ComandosScript { get; private set; }
        public int Size
        {
            get
            {
                int aSumar = 0;
                int total = 0;
                bool? isEndOrReturn;
                for (int i = 0; i < ComandosScript.Count; i++)
                    total += ComandosScript[i].Size;
                if (ComandosScript.Count > 0)
                {
                    isEndOrReturn = EsUnaFuncionAcabadaEnEndOReturn(ComandosScript[ComandosScript.Count - 1]);
                    if (isEndOrReturn.HasValue)
                    {
                        aSumar = 1;
                    }
                }
                return total+aSumar;//le sumo el End/Return
            }
        }
        public IEnumerable<Script> GetScritps() => ComandosScript.Filtra((c) => c is IScript).Select((c) => (c as IScript).Script);
        public IEnumerable<BloqueString> GetStrings() => ComandosScript.Filtra((c) => c is IString).Select((c) => (c as IString).Texto);
        public IEnumerable<BloqueMovimiento> GetMovimientos() => ComandosScript.Filtra((c) => c is IMovement).Select((c) => (c as IMovement).Movimiento);
        public IEnumerable<BloqueBraille> GetBrailles() => ComandosScript.Filtra((c) => c is IBraille).Select((c) => (c as IBraille).BrailleData);
        public IEnumerable<BloqueTienda> GetTiendas() => ComandosScript.Filtra((c) => c is ITienda).Select((c) => (c as ITienda).ListaObjetos);
        #region Interficies
        #region ILastResult implementation
        public IList<object> LastResult
        {
            get
            {
                ILastResult lastResult = null;
                ILastResult aux;
                for (int i = 0; i < ComandosScript.Count; i++)
                {
                    aux = ComandosScript[i] as ILastResult;
                    if (aux != null)
                        lastResult = aux;
                }
                return lastResult != null ? lastResult.LastResult : new object[0];
            }
        }

        #region IEndScript implementation




        public bool IsEnd
        {
            get
            {
                IEndScript iEnd = null;
                IEndScript aux;
                for (int i = 0; i < ComandosScript.Count && iEnd == null; i++)
                {
                    aux = ComandosScript[i] as IEndScript;
                    if (aux != null && aux.IsEnd)
                        iEnd = aux;
                }
                return iEnd != null;
            }
        }

        /// <summary>
        /// Es el valor que tiene al leerse de la rom si es null es porque acaba con un comando IEndScript
        /// </summary>
        public bool? IsEndFinished { get; private set; }

        #endregion

        #endregion
        #region IBloqueConNombre implementation
        public string NombreBloque
        {
            get
            {
                if (Equals(nombreBloque,default))
                {
                    nombreBloque = "script" + DateTime.Now.Ticks;
                }
                return nombreBloque;
            }
            set
            {
                nombreBloque = value;
            }
        }
        #endregion

        int IComparable<Script>.CompareTo(Script other)
        {
            int compareTo;
            if (other != default)
            {
                compareTo = IdUnicoTemp.CompareTo(other.IdUnicoTemp);
            }
            else compareTo = -1;
            return compareTo;
        }

        /// <summary>
        /// Obtiene los Bytes de la declaración y los offsets son temporales, tienen el IdUnicoTemporal del script como offset
        /// </summary>
        /// <param name="endOrReturn">si el utlimo comando no determina un end/return se pondrá el utimo byte dependiendo de esta variable</param>
        /// <returns></returns>
        public byte[] GetBytesTemp(bool endOrReturn=true)
        {
            byte[] data = new byte[Size];
            int offset = 0;
            
            for(int i = 0;i< ComandosScript.Count; i++)
            {
                data.SetArray(offset, ComandosScript[i].GetBytesTemp());
                offset += ComandosScript[i].Size;
            }
            
            if (offset < data.Length - 1)
                data[data.Length - 1] = endOrReturn ? END : RETURN;

            return data;
        }
   

        #endregion
        public static int GetIdUnicoTemp()
        {
            return GenId.Siguiente(GenId.Actual().NextOffsetValido() + 1);//así siempre no será valido y no tendré problemas
        } 
        public static bool? EsUnaFuncionAcabadaEnEndOReturn(Comando comando)
        {
            IEndScript comandoEnd = comando as IEndScript;
            return comandoEnd != null? comandoEnd.IsEnd:new bool?();
        }
    }
}
