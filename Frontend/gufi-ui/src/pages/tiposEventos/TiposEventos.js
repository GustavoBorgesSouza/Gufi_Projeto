import { Component } from "react";

export default class TiposEventos extends Component{
    constructor(props){
        super(props);
        this.state = {
            listaTiposEventos : [ {IdTipoEvento : 1, titulo : "C#"}, {IdTipoEvento : 2, titulo : "ReactJS"} ],
            titulo : ""
        }
    };

    componentDidMount(){
        //
    }

    render(){
        return(
            <div>
                <main>
                    <section>
                        {/* Subtítulo da página */}
                        <h2>Lista de Tipos de Eventos</h2>
                        <table>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Titulo</th>
                                </tr>
                            </thead>

                            <tbody>
                                {
                                    this.state.listaTiposEventos.map((tipoEvento) => {
                                        return (
                                            <tr key={tipoEvento.IdTipoEvento}>
                                                <td>{tipoEvento.IdTipoEvento}</td>
                                                <td>{tipoEvento.titulo}</td>
                                            </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </section>
                </main>
            </div>
        )
    }
    
};

