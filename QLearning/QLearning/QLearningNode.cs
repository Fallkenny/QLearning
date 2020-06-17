using System;
using System.Collections.Generic;
using System.Text;

namespace QLearning
{
    class QLearningNode // representa o nó
    {
        public int Reward { get; set; } // Recompensa ao chegar ao nó

        public int Id { get; set; } //Identificador
    }

    class QLearningObjs
    {
        Dictionary<int, Dictionary<int, int>> QTable { get; set; } // A Tabela Q será representada por um dicionário, 
                                                                   // que será indexado pelo Id do nó origem
                                                                   // Informando o nó origem, chegaremos a outro dicionário, 
                                                                   // onde o nó destino será informado e assim tendo 
                                                                   // o valor da recompensa atual para esse movimento
        QLearningNode[,] Map { get; set; } = new QLearningNode[10, 12]; // Representação do mapa, como uma matriz de Nós
                                                                        // Os espaços "não usados" receberão nulo
    }

}
