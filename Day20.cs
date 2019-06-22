﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC2018
{
    public class Day20
    {
        private readonly Dictionary<(int x, int y), HashSet<char>> _validDirections = new Dictionary<(int x, int y), HashSet<char>>();

        public string CalcA()
        {
            const string test0 = "^WNE$";
            const string test1 = "^ENWWW(NEEE|SSE(EE|N))$";
            const string test2 = "^ENNWSWW(NEWS|)SSSEEN(WNSE|)EE(SWEN|)NNN$";
            const string test23 = "^ESSWWN(E|NNENN(EESS(WNSE|)SSS|WWWSSSSE(SW|NNNE)))$";
            const string test31 = "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";
            const string data = "^NNNEESWSSENENNEESWSESWSW(WSEESWSESWSEENNESSENESSWWWSEESENEESESSENENNESESWSEEENESSEENWNENWWWNNESEEEESESEENESSWSWWWW(NN(ESEEWWNW|)N|SSWWSESWSWWSEEESSSENESSESESSWWSWNNE(E|NWWWWSSWNNWNNENWWWSWSSWWWSSSENNEEENE(N(W|N)|SESWSESWWNNWSSSSEEEN(WW|ESESWSWWN(E|WSWSWWWSWSWNNENEEEN(E|WWWNNN(NNNWSWSWWSWNNENWWSSSSSESWWWNNE(S|NWWNWWNNNNNNWSWNWSWWNENWWWSESWSWNWWWSWWSWSWNNNE(S|NWWSWSESSWNWNWNNNNNWWWSWWSWWWNENE(S|ENE(NENWNWNWNENESSESENNNW(S|NENWNWSSWWSWSWNWWNEEENENWWSWNWWWWSWWNWWNENNESSE(NNNEENWNENNWNEENESSWSSSSESWSWS(WNNEWSSE|)EENENNENESSSENENWNNWNWNEEEEESEENWNWNENESES(SSESSEENNNNESEEENENWNNWWNENEEESSEESSENENWNNEES(SSSWSEEEENWWNENNEENENWNWWWS(EESWWSS(NNEENWESWWSS|)|WWNENEENWWWSWSSWW(SESWENWN|)WWWWWWS(WSESWSS(WNW(WNENWNENWNNNWNENNWWS(WWNENENWWWS(E|WNNWSWNWSSESWSWNNNWWSSWWSSSWWWSESWSESWWWSESWWSSEESSESSESE(SWWNWNWN(E|NWWSWWWSESWWSSWSEESWSSENESEESESSWWSESSENNEEEENNWNE(EESSSW(NN|SSEEESEESSWNWSWNNWSWNWNNWSSSSSWSSEEN(W|ESSESWSESWWSWNNWNWWNNNE(SSEESE(N|S)|NWWNWWNEN(ESENESE(SWW|NNWNE)|WNWWSESWSSE(N|ESSWNWSWSSEEEESWWSEESWWSSSEEEEENNNN(ESSESWSSESSWSEESWWSSWWNWWNNWNNN(EESEE(NWES|)(SWSES(WWNNWN|S)|E)|WWNNWWWSWWNENENNWW(S(S|E)|NENNEE(SWSEESSE(SWWNNSSEEN|)NN|NWWW(NEENNENWWNEENE(SSSSSWN(SENNNNSSSSWN|)|NWWSWW(NENWNNNNESEENE(SESSS(WNNWSW(NWS|SE)|SEE(ESNW|)NWNENN(ESE|WS))|NNWSW(S|NNWW(SSENSWNN|)NEENE(SS|ENNENWWSSWNWSW(SEWN|)NNENNEES(WSNE|)ENEEEE(SWWWEEEN|)NE(SEEWWN|)NWWNENE(S|ENWWNNEEES(WW|ENNWWNEENWWNENNNENENESSWSWSES(W|ENE(SSWWEENN|)N(NNESE(SWEN|)NNEENWNNNWSSSWNWWS(ESEWNW|)WWWWWSES(ENEWSW|)WWNNNENEENNNWWNWNNWNWWSWNWNEEENESEEEESSSS(ENNEENWNENWNEEENNWSWNWNNNEEEEEESEESEEENENNESESEEENNWSWNNW(S|NNNWWWSESWS(EENNSSWW|)SSWW(SEWN|)WWNNNWNENEESSW(N|SEE(SWWEEN|)NNNNWNNWNEESEEEEENESSWS(SENEEENNNWW(SESWENWN|)NWWWWS(E|WNNWNWNNN(WWWWWSEESSEE(NNWSNESS|)SES(ESNW|)WWWWWWWNNWSWSSWSWNNENNENENN(WSWWN(WWWWWSEEEESWWSSESSE(SSSSSWNNWSWWSESSWSSSENNEEESE(SSWSES(SWS(ESSNNW|)WWWNWNNESESEN(E|NWN(WWWNNNWSWNWNENESEN(NNWSWNNENWWSWNWNW(SSESWSESWSSSSEESEESSWNWSSS(SSWNW(SSSEEE(NWWEES|)ESSWSSEESWWWSWW(NNE(NNE(SS|N(WWSSNNEE|)E)|S)|SSEESWSSENEEESWWSSESENN(W|ESEESWWSEES(ENNNNNN(ESSNNW|)WSW(SEWN|)NNW(SS|WNENN(E(NWNEE(N(ESENSWNW|)WN(WSW(W|NNEENW(N|W))|E)|S)|SS)|WSWW(NE|WW|SESW)))|WSWNWWN(WSWSESSWNWSW(NNNNE(SS|NE(S|NWN(W(NNN|SS)|E)|E))|SESSENESES(WWWW(SS|NN)|EE(E|NNW(S|WN(NNNESSENESSWW(EENNWSNESSWW|)|WW)))))|E))))|NNNE(NNWSNESS|)SS)|ENESENENNW(NNWWNWNW(S|NENENNWS(NESSWSNENNWS|))|S))|NNESENESEES(EEENWNE(NWWS(WNW(S|WNWWNW(SSEEWWNN|)NEEN(WW|ESEEE(NWWEES|)SEESWWWNW(WW|S)))|S)|ESSESWWSWWNE(WSEENEWSWWNE|))|WWW))|E)|EN(NWSNES|)E))|E)|NNN(WWW(SEEWWN|)N|ESSEENNENESE(SESWWNWS(NESEENSWWNWS|)|NNNWNWWW(WSESS(WWN(NN|E)|ENE(NWES|)E|SS)|NEEN(WNSE|)EES(ENESSW(S(EENSWW|)S|W)|W)))))|NNN(W|E(NEWS|)S))|E)|EESWSESSENE(NW|SEE))|ESEEESEENN(ESESSS(WN(WWWS(WNWN(E|W)|EE)|N)|SEESEENWNNENEEEEESSWNWWSW(N|W|SEE(ESWSSWSWWWW(SEESSEENEN(WWSNEE|)NESEENWNENENNW(NEENWNENESEEEEEENN(WWWWW(SEEEEWWWWN|)WWWWS(E|WNWWS(E|WNWWS(E|WNWW(W|SSSSENN(E|N))))|SS)|ESSSENNEEESENN(ESSEENWNEESEESWSEENEEENESESWSWNWWSESWWWWWN(EEE|WN(ENSW|)WWWSSSSENEN(NWSNES|)ESEEESSESSWSWNWWNNEE(NWWWSSSWWSWNNEN(ESNW|)WWWWWSSSENNESE(N|SWSWWWNWWWWWSWWWSSEESENN(ESSENENWN(W|EESSSEESESWWSSSWSESESWWWSSSSSSSSESSSWWSESWWSESWS(WNNWSWWNNE(S|ENWNWWWNEENESENESS(W|S(S|ENNNNE(SESWENWN|)NWWSWNWSWNNNWSWNNNEENNENESEENE(NE(NWNNNEE(SWSNEN|)NN(W(N|WWS(EE|WNWWSESWSESEE(NNWSNESS|)SWWWWWWSWWSSWWWNNESENNNESENNNWSWNNENNWSWWSESSWNWWSESEESWWWNWWNENWN(WWSSE(SWSESWSWSWWWNWSSWNWSSWNNWNNESENE(ENENNN(WSSWWNWN(EESNWW|)WNWWSSW(SSWWSEEESSWNWSW(SWWSESEEN(NESEESWWS(WSWS(WWW(NEEN(WNWSNESE|)E|S(WW|E))|E)|EEEN(NNW(W|NNNNN(WSNE|)E(NWNSES|)ES(W|EE))|ESSSSW(WNNESNWSSE|)SEENEENWWNNEN(ESEESESEESSESSSEESEEENEEESEESSSE(NNNN(E|WWNNNWSSWWNWWNWSWNW(NENNNNENEEN(WWWSWNWSWSSW(WNENWWSWN(NEEEENEEENEE(SWEN|)NWWNWSSW(ENNESEWNWSSW|)|WW)|SEES(S|ENNNWS))|EEEE(SWSESSSWNNWNNWSWSW(WSEEE(N|SE(N|S(WW(SEES|NWWSE)|EEEE(NW|SW))))|N)|NEN(W|E(SSWENN|)EN(W|EEE(E|S)))))|SSEEE(SWEN|)E))|E(EE|SWSSWWWNEENWNWWS(E|WSS(ENSW|)WNNNWWWNN(EES(ENEE(NWES|)S(WS|EENW)|W)|W(SSSEESE(N|SWW(WNEWSE|)S(SSS|E))|NWWNNWN(E|NWWSS(ENSW|)WNN(NE(NWNWWEESES|)EE|WWS(WWWSWW|E))))))))|W)))|W)|N)|NNWWN(WSNE|)EEE)|EN(W|N|ESE(SWWSSS(W|E(SWEN|)NE(NWES|)S)|N)))|S)|N)|NNNEEEN(EEEENESESW(SSENE(NNNWN(EEENNW(NENEESENENNEEEEN(ESESS(EENNNNWW(NE|SESS)|WNWWWSSSE(NNEWSS|)SWWNW(NENNSSWS|)SWWW(S|N(EE|N)))|WNWWS(E|WNWSSWS(E|WN(WS|NNES))))|S)|W)|E(SWSSE(N|SWW(NNW|SEEE))|E))|WWWSW(N|WSSWWNNES(NWSSEEWWNNES|)))|WWWNNE(EESWWEENWW|)NWWSWWSESEN(SWNWNEWSESEN|)))))|E)|E)|SSSWWN(WSW(N|S(ESES(WWNSEE|)EENWN(W|E)|WW))|E)))))|EENEESESSEESESWWSESSWNWNNW(N(EE|WNWN(WSNE|)E(ESNW|)N)|SSWSWWSESEE(N(E(NWES|)EEEEEEEEEESEEESENNNEEENWNEEEESESSEENWNENNNWWNWNEENWNNNWSSWS(WWSWNWNWSSWSESES(SSWNWWWSESS(ENNSSW|)WNWNNWWSESWWWNWWN(WSSEEWWNNE|)ENENNWSW(NNNEES(W|ENNESESWSSW(S(EEENESEN(ESEWNW|)NNWSW(WSNE|)NNNNW(NEESSES(ENNNNNESEENENWW(S|NNESEEEESWWSES(WWSSS(ENENWESWSW|)WNWWNEE(WWSEESNWWNEE|)|EEN(W|NENENESESWWSWSSSSESSS(EEENWNNESEEEESEESWWWNWWSSWSWNWSSESSEEESSENENWNEESSESWSSWN(N|WSWWN(NWSSSSEESSWNWSWWNNWWWWSEESSWSESWS(EENENWN(NN|EEESENESSWSS(WNNW(SWS(W|S|E)|N)|SSESEEENNNWWW(SESENSWNWN|)NEENWNEENWNEENNN(WSSWW(NENWWW|SS)|NNENNENWWNENNN(WWSS(ENSW|)WNWN(W(N|SS(E|W(S(WNSE|)E|N)))|E)|EEENWWWNNEEEEENEEESENEENWNWNNNWWWNWNWSWWWWSWNNNNWSWSSSSWNNNWNENNWWS(SSSW(SSSSWWS(WNNENES(NWSWSSNNENES|)|EEESENNN(ESSSENNNEEN(WNNNSSSE|)EENEESESEN(NWES|)ESSWWWWN(E|WWSWSES(EEN(WNSE|)EE(SWEN|)EEEE(NNNWSS|E)|WSWNNN)|N)|W(NNE(N|S)|S))|SSS)|NNNWNNNNWWNEEEEESSES(ENNEEESSS(WNW(NE|SWS)|ENNNEENWWNENWNEESSENESSEESSSSWWNENWN(W(SSWNWSSESE(S(EESEENEEENENWWNENNEENWWWSSWSSS(W(NNNNNNNWW(SEWN|)NWNNNNNWNWSWSSWSWNNWWSESWSEE(EENESE(NNNWSNESSS|)S|SWWSEESWS(WWW(NWNNWSWWNENNWSWNWSSWNNWWNWSSSEE(NWES|)SWSEE(SSWNWWNNWWNWNENE(SS|NWNWWSS(ENSW|)WWSESSWSWNWSSEEENEN(NN|ESSSEE(NWNEWSES|)SWSSESEESWWW(SEEEE(NNNNN(EESE(N|SWW(N|SES(WSNE|)E(ESNW|)N))|WW(NEWS|)S(SENSWN|)W)|S)|WWNWWNEEE(SEWN|)NNWWN(ENSW|)WWWWNNWNWWWNNWWWNWSSW(NNW(NEEEENESES(WW|EESENNWNW(S|NNNENEESENEESENESESES(EENEENWWW(NNESENNEEESENENNWSWWNENEEESENNWWWNNEENNWNNWWSWNWN(WWWW(SESSSWSESSWSWWNEN(E|W(NEWS|)WSSW(NWNEWSES|)SEEEESS(SW(SESNWN|)WNWW(NEEESNWWWS|)S(W(N|WWWWWN(WSS(WNWWSESWWS(WNN(WWSEWNEE|)E|ES(SWNSEN|)EEE(S|NW(NEWS|)W))|E)|EE))|E)|EENWNNESESENNNW(S|NEEE(SWSNEN|)NWNEN(E(SEWN|)N|WWNWNWSSSE(N|S(WSS|EN))))))|WWWSWNW(WW|S))|EEEEEESWSESENNESSSSSSSSW(NNNW(NENWESWS|)WW|SWWSSSSWWWSW(SESWS(WNNWSWW(EENESSNNWSWW|)|EEEENEENNNW(NENEN(WWSNEE|)ESSENEEESEENWNWWWNNEES(ENNWWNNNWNEENNENEN(WWSWWN(E|WW(SESES(ENSW|)WW(SESSW(N|SES(ENNSSW|)WSES(S|WW))|N)|WW))|ESSWSWSSEEN(W|NENENWNEEEEEEEESSSWNNWWSSSSEN(NN|EESWSWWSSSSSSWWSWNWWSW(S(WWWWSWN(NEWS|)WWW(N|SES(E(EEE(NEWS|)S|N)|WSSE(N|SSSS(SEWN|)WNNWWNE(NWES|)E)))|EE(N|ESEEENW(NEENEESSW(N|WSEEENNE(NNNNWSS(S|WWNW(S|NEE(S|NWN(WSNE|)EEN(W|E(SSWENN|)NWNENNNW(NEWS|)SS))))|SSSWWSESE(SWWSWSWWNNNNN(ESSE(SWSNEN|)(NN|E)|WSWWSESE(N|SWW(SEESES(WSSNNE|)ENESEEE(NNW(SWN|NE)|SWSSWS(WNSE|)SSSWSSSENEE(SSSWSWSSW(NNNN(WS(WNNNW(NEESSNNWWS|)WW|S)|E(ENSW|)S)|SWWSSSWSWNNWSSWNNN(NESEES|WW(WWW|SSSE(NN|SWWSSW(SSSSSWW(NENNSSWS|)SSSSSESSESEENNNNNEESENNENNESESESWW(N|WSESWSSESSSWWSWWSSWSESESSWNWNWNNWWWWSWWNEN(WWSSSEEESWSWSWNWWWWNENNWSWSSSSSS(WNNNWNNW(S(SSEWNN|)W|NEE(SS|NEN(WWSNEE|)EENE(N|SE(N|SWSSE(N|EE)))))|SESENNWNEEEN(EEEEN(ESSSSSEEESEESWSWWWNWN(EESEWNWW|)WNNNNWWSSWN(WSW(SEESEE(N(NN|W)|SESESSWSWNWWNWNN(EES(ES(W|E)|W)|NWSWSSSWNN(NNEWSS|)WSSSWNN(N|WSWWW(SEESWSSSWSSWSSESENN(EEEESWSWSSSESEESSESENNNW(NEENESSWSSSENNEESWSSEESS(WWWWWWWNN(ESENEES(EE|W)|NWNWWN(EE|WSSSESS(WNWNWWWSWS(EENESE|WWWWWNNWWWSWS(EEENWESWWW|)WWNENNE(S|NESENEES(W|ENESENESE(ENESEENNW(S|NENE(SEWN|)NN(NNEWSS|)WWS(WS(SWS(W(S|WWNNWWSS(ENSW|)WNWNWNEE(S|EEENENWWSWNNENWWSWNWSSS(ENESNWSW|)WNNWWNNNE(SSENEEEEENN(WWS(W|E)|ENEEESE(SWWSWW(NENEWSWS|)SSENESE(SWSSE(NENSWS|)SWW(SEWN|)N|NN)|NEN(W|N)))|NWNWN(WWWSSEE(NWES|)ESSSSWSEEESWWWWNNNENNWSWSSSSSWWNENWNWNWSWWSSESSSSWSESSESS(WNWNNWWSWNWNWWSSSEN(N|ESS(EEE(N(N|WW)|E)|WWWNNNWWNEENNEEEEEE(ENNWNNNWSWSESWS(WWNWSWNWWWNWSSWNWNENWNWWSESWWSSSESWWSSESEEEESS(EENESENNWWW(S|NENNN(WSWS(E|SWNNN(W(SSW(SEWN|)W|N(WNEWSE|)E)|E))|NEESWSE(WNENWWEESWSE|)))|WNWSWNWWNWNWNENWNWNEEE(SWEN|)NWNENWNEE(NNWWS(E|WSWNWSSWSES(ENNESS|WWWWSWSSESEESWSSWWNN(ESNW|)NWNNWNWSSESSESSWNWNNWWWSWNWWWSSENESEEENESSWS(WNWSWNWSWWWWWWNWSWWWWNENENWNWNEEENENEENEENWNENN(WN(WNNWSSWSEES(E|WSWNWNWNENWWSSSESWSWSW(NNNE(NNNW(SS|NENWNNNEESEE(NNWSNESS|)EESSWWWS(EESNWW|)WNN(WNSE|)EEE)|S)|SS(ENEE(SWEN|)ENEN(E(ENSW|)S|W(WSWENE|)N)|SSSESWSS(NNENWNSESWSS|))))|E)|ESSESESENESEENNW(NWSWNN(WWSESNWNEE|)NNEEESS(WNWSNESE|)SESSS(SSWNWSS(EE|WNNWWSWWWN(EN(ESNW|)N|WSSEE(EEENSWWW|)SSWNWWNW(NEWS|)WSESWW(NWES|)S(SWEN|)EEEE(NWES|)EES(WWW|E)))|ENNEEENWWNW(S|WNNNWNW(S|NWNENWN(W|EEESWSSEES(W|EESWSS(WNNSSE|)ENEESS(EENE(EEENENESENEENNWNEENWNENESENEEENWWWWN(WWS(WS(E|SSWSSWNWSS(WWWSE(SWWWW(NNENNENEENEENNENN(WN(E|WWNEENNNW(NEWS|)SSWNW(NEWS|)SSSSSSWWNENNWSWWSESSSE(SWSSWNNWWWS(SENESSSEN(SWNNNWESSSEN|)|W(NNENWNWSWWWNEEN(ENENWNEENWNEEE(EESEE(NWES|)SWWSWNWSW(NNEEWWSS|)SESSSS(ENNSSW|)WNNNW(NN|S|W)|NN(NNE(S|NNNWN(EESESESWSWNN(SSENENSWSWNN|)|WSW(SEESNWWN|)NWSW(W|NNENN(WSNE|)E(SSEEWWNN|)N(E|W))))|WWWWS(EEE|W(SESNWN|)NNWW(SEWN|)N)))|W)|W))|NEE(SWEN|)ENE(S|N(NWSNES|)E)))|ES(E|SSSW(N|SWWSWWS(WSSWENNE|)EE)))|S)|E)|EEE))|E)|EEEEESSEESENESENENWWNWSWNW(NWN(NWSWWS(EE|W)|EEENESENEEESWWSW(SEESE(NNWESS|)SWSWSWSSWWWSSWNWWSWSEE(N|ESWSWWWSS(WNNW(S|WNN(NEEE(NNNWN(EESENEE(NWWWWEEEES|)S(WS(WWSNEE|)E|EEENWW)|WSSWSEEN(SWWNENSWSEEN|))|SS(WWNEWSEE|)EE)|WSW(SEWN|)N))|EEN(W|ESS(WW|EEE(S|NNNW(SSWNNSSENN|)NNESENEENN(EEESSS(ENNNEN(WNWNW(SW(WSEEEWWWNE|)N|NEE(NWES|)SE(S|N))|ESSWSE(SWSESNWNEN|)E)|WW(NNESNWSS|)WS(W(WSNE|)N|EE))|WWS(WNSE|)E))))))|WWW))|S))|SSWWWSEESWWWWWSE(E|S))|W(N|W)))))))|S))|EE(N|EEEEEEEEENWWN(WSNE|)ENWNEN(WNWW(SE|NEEES|W)|ESSSESSE(EE|N))))))|S))|EE)|SSWWN(WWW|E))))|ENEENEEE(SSWNWSWW(EENESEWNWSWW|)|NNWSWNWNNWWN(W(SSEESS(E|W(NWS|SE))|NENNW(W|S))|EEEEESWS(WNSE|)EEE(SWSSNNEN|)ENE(NWWSWNN(EEE|WW)|ES(E|W)))))|ENES(S|ENNWWN(W(SS|W)|EEN(WW|N)))))))|E)|E)|E))|SWWS(WWNEWSEE|)E))))|ENN(NW|ESS))))|EENWNEENWWW(W|NEENNWW(SEWN|)NENNWWNNEES(W|ENESSWSSENESSWSES(W|EENENWW(S|NNEENWWWNNENEENWWWSWNNWNEENNWNNNN(EESESWW(SEESWSSEES(WWW|ESSSWWS(W|EEEEESESWWWSSW(N(NNEEWWSS|)WW|SSSW(N|WSEESS(WNWSWNW(NNES|SWN)|ENNESSENNEEESES(WWNWSNESEE|)ENNWNENWNNWWWSWNW(NEEEENNEE(SS(WNSE|)SS|NNWWS(E|W(SS|WNENWWSWNW(S|NENEENEEESSS(WNNWSWW(EENESSNNWSWW|)|ENNNNWWWNWSWWNWWW(S(W|ESE(EE|N|S))|NENNNNENWW(NEEEESSW(SSESES(EESEENWNNWNWS(SEWN|)WN(W|NNEEENWNENWWSW(SEWN|)NWWS(E|WNWNNENEESWS(W|EEEENWWNNENENWWSWNNEENESENE(NWWNNENWNENE(SSSSWENNNN|)NWWNEENNWWW(WSW(S(ESSE(NNNEEWWSSS|)(SWW(SEESWSWWN(E|WSSSE(EEN(ENSW|)WW|SWW(SSENESS(WWSWNWWSESESSS(WWNWWNNNNW(NW(W|NNNEESS(WNSE|)ES(E|SSSES(ESNW|)W|W))|SSSS)|E)|E)|NWNENW)))|NN)|E)|W)|N)|NNNNNEESE(SSSWWNENWN(SESWSEWNENWN|)|NNWNNE(S|NNNNNNNWW(NEENWNEN(SWSESWENWNEN|)|SSWSEE(SWSWNWSSWWNN(E(NENNENW(ESWSSWENNENW|)|S)|WSWW(NEWS|)WSSW(NN|SSW(SEEESWSE(ENNENWNW(SWNSEN|)NEN(ESSENEEENE(ENSW|)SSWWWSS(WNSE|)SE(NNEEWWSS|)S|W)|SWWWWN(ENESNWSW|)WSSEESSW(SES|NW))|NN)))|NN)))))|SSWSSE(SSS(WNNW(N|W)|SSSSSW(SESSNNWN|)WW)|N)))))|W(S|WWN(E|NN)))|N)|SWSESS)))))))|SWSES(ENESEENNWS(NESSWWEENNWS|)|W)))))))|N)|WSSSW(N|SS(E(E|N)|SSWSES(ENSW|)WWWSWSS(E(SSE(SW|NE)|N)|WNWSSWWNW(S(SE|WN)|NEE(NENNES(S|ENNWWNNWSSSSW(S|NWWWWWNENEES(W|ENE(NWNN(WW(N|SESWWN)|ESENEEN(ESSEN(EESWSS(ENSW|)W(SSENSWNN|)WN(W(NWES|)S|E)|N)|W(N(E|NN)|WW)))|S))))|S)))))))))))|S)|W)|WNN(WSNE|)ENE(NWES|)SS(W|E)))))|N)|N)|W)|WN(WWSEWNEE|)E))|ENESENNN(WSWNNE(E|NNW(SWNSEN|)NN)|ESESS(WNSE|)ENNEN(WW|NNNEN(WNSE|)ESSWSEESWWS(NEENWWEESWWS|)))))|NNW(NEEWWS|)S)))))|NW(NENNN(WSSNNE|)NN|W)))|NW(S|NNN(NWES|)EE))))|N)))|W)))|NNWNNEENESESWS(WNWESE|)EENNNWNNNWW(SWWWS(WNNSSE|)EE(SW|EEN)|NEEN(W|ENWNEESSSWSSSE(NN|SSSS)))))))|W)|SWS(E|WW(SE|NE))))|NN(EEEN|WNE))))|S)|WWNWWW(N|SWSSE(SWW(NNWNN(WSNE|)(E(E|S)|N)|SES(W(WWWNSEEE|)S|E(N|E)))|NENESSW(ENNWSWENESSW|))))))|SS)|SEEEE(NWWEES|)SEESE(ESWWWSWWNEN(E|WNWSWSESWWWNEN(NEWS|)WWWSSE(N|SSSESSSENEENWNNN(EESS(WNSE|)SESWSSEEENNW(SWEN|)NENNWSWNNEENEEESESWSEE(NNE(NWWEES|)EE|SESWWSSE(EN(W|E(S|NN))|SS(WNWSWWWWSS(EENWESWW|)WNWNEN(EEEENENNN(E|WW(NNNESS|SS(ENSW|)S))|WWWN(ENWESW|)WSSWNNW(SSSES(WW(NN|W)|ENENESSWSW(W|SSENE(NE(EE|N)|SS)))|NNNWNNN(SSSESSNNWNNN|)))|SS)))|WWSESS)))|N)))))|EN(W|E(SEEEWWWN|)N))|S)|E))|W(WW|S))|E)|WSWNNWSSWNN(SSENNEWSSWNN|))|N)|N)|E))|WWW(NENWESWS|)S))|E)))))|WWNWWWNNEENE(NWWSWWNNE(NWN(NENWNEEENE(NWES|)SE(N|SWSWSSE(SWWNN(NNEWSS|)W|EEENENESS(W|E(NNNN(EE|W(SWWSW(SW(N|W)|N)|NN))|SS))))|WWSWSSWWNNNE(SS|N(ESNW|)WWWN(WSW(N|SSEESWWSES(WWNNSSEE|)EE(EEEESWWSWSW(SEENES(S(S|W)|ENESE(NNW(WW|N(E|NNN(WSNE|)N))|ES(W|SS)))|NN(E|WW))|N(NNNWWEESSS|)W))|E)))|S)|SS(E(NNEWSS|)S|WW)))|E))|W))))|W)|S)|S(W|S))|N))|S)|EEENN(NESSES(EESWENWW|)W|WW(SE|WN)))|E)|W)|SSSSEESWWWSEESSSWSWWSSEEESWWWSEESEESSSSWNNWWN(EE|NWWSSSE(NN|ESWWSWS(WNNWNENE(NWWSWWNWSSSEE(NWES|)SS(ENSW|)WWWSWWWNENE(E(EE|NNWNWWSS(WWNENWWSWNNENEEES(EENE(SS|NWNEEEEEENWNENESE(SWSESWSWNWWS(WNWSNESE|)(S|E)|NNNNE(SSENNES(NWSSWNSENNES|)|NWWWSSWWWNNNNWWNWSWSEESE(SWWWSSSSWNWSSWWSS(ENEEE(S(WW|S)|N(W|ENNNEN(W|ESESWWSSEEN(E(S|NEN(EENNSSWW|)W)|W))))|WSWWNNNENE(SSWSNENN|)NEE(SWEN|)NENNNE(SSS|EE|NWNWW(NEEE(ENEEN(EESWSEENEESSEES(ENNWWNEEE(NWES|)SS|WWWN(WSSESW|N))|WNN(ES|WNNW))|S)|SESWWWSWWN(E|WWSSS(EESW(W|SE(SSSW(SSE(N|EESWSESW(WWW(NNEESW|W)|SSEEN(W|NESESE(NEWS|)S(WW(N|WWSW(N|S))|SE(EEEEN|SW)))))|NN)|ENENEN(E(ENWWEESW|)S|WW(WWNSEE|)S)))|WNNNNW(SSSSW(SS|N|WWW(NN|W))|N(EEES(ENSW|)W|NW(SS|W))))))))|N))))|WW)|E(E|N)))|S)|SS)|ES(W|EEESSSSESWSEESENNNENWW(NNW(SS|NNWWN(WSNE|)E(EESSEEEENWNNESEESWSSSEENWNENNNNWW(SEWN|)WWNNWSSW(NNNNW(W|NENNN(ESSSEEN(WNNSSE|)ESSWS(WNWSNESE|)SEEN(EENWN(WSNE|)E(ESSEEE(N|SEESSEES(ENNN(WW(SEWN|)N(E|WW)|EE)|WWSWWNENWWSSWSSENEES(WSWWSESWWW(SWNWWS(ESWSNENW|)WWNNWWNEEESS(NNWWWSNEEESS|)|NNE(S|NWNNNNEE(SWSSNNEN|)NN(ESEEWWNW|)WSWNWSWWW(EEENESNWSWWW|)))|ENE(N|SEEESWSS(E(EE|N)|W)))))|N)|W)|W(WS(ESNW|)WW|NNE(NNWSNESS|)S)))|SSE(SWEN|)N)|NN))|SS)))))))))|WW))|S(E|W)))|WWWW))|WS(SWSNEN|)E)|NNEEE(ENW|SWW))|N)))|W(S|WWW))))|WWN(E|WW(SEWN|)W))))|WNWNENWW(EESWSEWNENWW|))|W)))))))|SSSSEN))|SS))))|WSSW(NNN(EE|NNNNW(SSWWWEEENN|)N)|WS(WNSE|)EE)))))))|NWWNWS(SESWSEE(WWNENWESWSEE|)|WNNNESENN(ESNW|)NWWS(E|WNW(NNEE(N|S(W|E))|WS(WNSE|)ESESSWNWN(SESENNSSWNWN|))))))|ENN(E|W(WNNWNN(NNESSSESE(SWEN|)NEENEENESEEEENWWWNNNWNEEESSS(WNNSSE|)EESE(ENWNNNWSSWNNNWNEEEE(S(SSE(NN|S(SS|W))|WW)|NWWWWWN(EENWWN|WSWSS(EENWESWW|)WNWSWNNN(WSSSSSESWWNW(NENWESWS|)SSESE(SWWNSEEN|)NEE(SWEN|)NNE(NWW|SSEN)|E(SEWN|)NN)))|SWSWS(E|WWN(E|WWS(E|SSS(SWNNSSEN|)E))))|WW)|S))))|E)|SSES(W|SS))|ENEESSWN(SENNWWEESSWN|))|E))|W)|W)|S))|S))))|EEE(SSWNWS|NN))))))))|N)$";

            var test = data;
            var enumerator = test.GetEnumerator();
            enumerator.MoveNext();
            var tok = Parse2(enumerator);

            tok.Walk(this, new[] { (0, 0) });

            var visited = new HashSet<(int x, int y)>();
            var b = new HashSet<(int x, int y)>();

            var queue = new Queue<((int x, int y) pos, int depth)>();
            queue.Enqueue(((0, 0), -1));
            int curDepth = 0;
            while (queue.Count > 0)
            {
                var (pos, depth) = queue.Dequeue();
                curDepth = Math.Max(curDepth, depth);
                if (!visited.Add(pos))
                    continue;

                if (depth >= 999)
                    b.Add(pos);

                foreach (var dir in _validDirections[pos])
                {
                    var newPos = Move(pos, dir);
                    queue.Enqueue((newPos, depth + 1));
                }
            }

            //Print();

            return b.Count.ToString();
        }


        private Token Parse(IEnumerator<char> inp)
        {
            var curTokens = new List<Token>();

            while (true)
            {
                if (char.IsLetter(inp.Current))
                {
                    var chrToken = new CharToken();
                    do
                    {
                        chrToken.Chars.Add(inp.Current);
                    } while (inp.MoveNext() && char.IsLetter(inp.Current));

                    curTokens.Add(chrToken);
                }

                else if (inp.Current == '(')
                {
                    inp.MoveNext();
                    curTokens.Add(Parse(inp));
                }

                else if (inp.Current == ')')
                {
                    inp.MoveNext();
                    return CreateToken(curTokens);
                }

                else if (inp.Current == '|')
                {
                    inp.MoveNext();

                    Token tok1 = CreateToken(curTokens);

                    var otherOrePart = Parse(inp);
                    if (otherOrePart is OrList or1)
                    {
                        or1.OrParts.Add(tok1);
                        curTokens = new List<Token> { or1 };
                    }
                    else
                    {
                        var or = new OrList();
                        or.OrParts.Add(tok1);
                        or.OrParts.Add(otherOrePart);
                        curTokens = new List<Token> { or };
                    }
                }
                else if (inp.Current == '^')
                {
                    inp.MoveNext();
                }
                else if (inp.Current == '$')
                {
                    return CreateToken(curTokens);
                }
            }
        }

        private Token Parse2(IEnumerator<char> inp)
        {
            OrList cur = null;
            var curTokens = new List<Token>();

            while (true)
            {
                if (char.IsLetter(inp.Current))
                {
                    var chrToken = new CharToken();
                    do
                    {
                        chrToken.Chars.Add(inp.Current);
                    } while (inp.MoveNext() && char.IsLetter(inp.Current));

                    curTokens.Add(chrToken);
                }

                else if (inp.Current == '(')
                {
                    inp.MoveNext();
                    curTokens.Add(Parse2(inp));
                }

                else if (inp.Current == ')')
                {
                    inp.MoveNext();
                    return CreateToken2(cur, curTokens);
                }

                else if (inp.Current == '|')
                {
                    inp.MoveNext();

                    if (cur == null) 
                        cur = new OrList();

                    cur.OrParts.Add(CreateToken(curTokens));
                    curTokens.Clear();
                }
                else if (inp.Current == '^')
                {
                    inp.MoveNext();
                }
                else if (inp.Current == '$')
                {
                    return CreateToken2(cur, curTokens);
                }
                else
                {
                    throw new Exception("knas " + inp.Current);
                }
            }
        }

        private static Token CreateToken(List<Token> curTokens)
        {
            if (curTokens.Count == 1)
                return curTokens[0];
            if (curTokens.Count == 0)
                return new EmptyToken();

            var ret = new TokenList { Tokens = new List<Token>(curTokens) };
            return ret;
        }

        private static Token CreateToken2(OrList orList, List<Token> curTokens)
        {
            var tok = CreateToken(curTokens);

            if (orList != null)
            {
                orList.OrParts.Add(tok);
                return orList;
            }

            return tok;
        }


        class EmptyToken : Token
        {
            public override ICollection<(int x, int y)> Walk(Day20 d20, ICollection<(int x, int y)> pos)
            {
                return pos;
            }

            public override string ToString()
            {
                return "";
            }
        }

        abstract class Token
        {
            public abstract ICollection<(int x, int y)> Walk(Day20 d20, ICollection<(int x, int y)> pos);
        }

        class CharToken : Token
        {
            public readonly List<char> Chars = new List<char>();

            public override ICollection<(int x, int y)> Walk(Day20 d20, ICollection<(int x, int y)> pos)
            {
                var ret = new HashSet<(int x, int y)>();

                foreach (var p in pos)
                {
                    (int x, int y) pm = p;
                    foreach (var dir in Chars)
                    {
                        d20.AddDirection(pm, dir);
                        pm = Move(pm, dir);
                        d20.AddDirection(pm, Opposite(dir));
                    }
                    ret.Add(pm);
                }

                return ret;
            }

            public override string ToString()
            {
                return new string(Chars.ToArray());
            }
        }

        class OrList : Token
        {
            public readonly List<Token> OrParts = new List<Token>();

            public override ICollection<(int x, int y)> Walk(Day20 d20, ICollection<(int x, int y)> pos)
            {
                var ret = new HashSet<(int x, int y)>();

                foreach (var part in OrParts)
                {
                    foreach (var p in part.Walk(d20, pos))
                        ret.Add(p);
                }

                return ret;
            }

            public override string ToString()
            {
                return '(' + string.Join("|", OrParts) + ')';
            }
        }

        class TokenList : Token
        {
            public List<Token> Tokens = new List<Token>();

            public override ICollection<(int x, int y)> Walk(Day20 d20, ICollection<(int x, int y)> pos)
            {
                var ret = new HashSet<(int x, int y)>();

                foreach (var p in pos)
                {
                    ICollection<(int x, int y)> pm = new[] {p};
                    foreach (var t in Tokens)
                    {
                        pm = t.Walk(d20, pm);
                    }

                    foreach (var p2 in pm)
                        ret.Add(p2);
                }

                return ret;
            }

            public override string ToString()
            {
                var ret = "";
                foreach (var t in Tokens)
                    ret += t;
                return ret + "";
            }
        }

        private void AddDirection((int x, int y) pos, char validDirection)
        {
            if (_validDirections.TryGetValue(pos, out var hash))
                hash.Add(validDirection);
            else
                _validDirections.Add(pos, new HashSet<char> { validDirection });
        }

        private static (int x, int y) Move((int x, int y) pos, char dir)
        {
            var (x, y) = pos;
            switch (dir)
            {
                case 'N':
                    return (x, y - 1);
                case 'S':
                    return (x, y + 1);
                case 'E':
                    return (x + 1, y);
                case 'W':
                    return (x - 1, y);
            }

            throw new Exception("Unknown direction " + dir);
        }

        private static char Opposite(char dir)
        {
            switch (dir)
            {
                case 'N':
                    return 'S';
                case 'S':
                    return 'N';
                case 'E':
                    return 'W';
                case 'W':
                    return 'E';
            }

            throw new Exception("Unknown direction " + dir);
        }

        private void Print()
        {
            int minX, maxX, minY, maxY;
            minX = _validDirections.Keys.Min(k => k.x);
            maxX = _validDirections.Keys.Max(k => k.x);
            minY = _validDirections.Keys.Min(k => k.y);
            maxY = _validDirections.Keys.Max(k => k.y);
            var sb = new StringBuilder();

            for (int y = minY; y <= maxY; y++)
            {
                sb.Append('#');
                for (int x = minX; x <= maxX; x++)
                {
                    if (_validDirections.TryGetValue((x, y), out var hash) && hash.Contains('N'))
                        sb.Append('-');
                    else
                        sb.Append('#');
                    sb.Append('#');
                }
                sb.AppendLine();

                sb.Append('#');
                for (int x = minX; x <= maxX; x++)
                {
                    if ((x, y) == (0, 0))
                        sb.Append('X');
                    else
                        sb.Append('.');

                    if (_validDirections.TryGetValue((x, y), out var hash) && hash.Contains('E'))
                        sb.Append('|');
                    else
                        sb.Append('#');
                }

                sb.AppendLine();
            }

            sb.Append("#");
            for (int x = minX; x <= maxX; x++)
                sb.Append("##");
            sb.AppendLine();

            Console.WriteLine(sb.ToString());
        }
    }
}