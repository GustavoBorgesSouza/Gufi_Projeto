import { useState, useEffect } from "react";
import axios from "axios";
import { render } from "@testing-library/react";

export default function meusEventos(){

    const [listaMeusEventos, setlistaMeusEventos] = useState( [] )

    function buscarMeusEventos(){
        axios('http://5000/api/presencas/minhas', {
            headers : {
                'Authorization' : 'Bearer' + localStorage.getItem('usuario-login')
            }
        }) .then( resposta => {
            if (resposta.status === 200) {
                setlistaMeusEventos( resposta.data )
            };
        }).catch( erro => console.log(erro))
    };

    useEffect(buscarMeusEventos, []);

    return(
        <div>
            <main>
                <section>
                    <h2>Meus eventos</h2>
                    <table style={{ borderCollapse: 'separate', borderSpacing: 30 }}>
                        <thead>
                            <tr>
                                {/* <th>#(idPresenca)</th> */}
                                <th>Evento</th>
                                <th>descricao</th>
                                <th>Data</th>
                                <th>Acesso</th>
                                <th>Situacao</th>
                                <th>Tipo de evento</th>
                                <th>localização</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                listaMeusEventos.map((minhaPresenca) => {
                                    return(
                                        <tr key={minhaPresenca.idPresenca}>
                                            {/* <td>{minhaPresenca.idPresenca}</td> */}
                                            <td>{minhaPresenca.idEventoNavigation.nomeEvento}</td>
                                            <td>{minhaPresenca.idEventoNavigation.descricao}</td>
                                            <td>{ Intl.DateTimeFormat('pt-BR', {
                                                year: 'numeric', month: 'long', day: 'numeric',
                                                hour: 'numeric', minute: 'numeric', second: 'numeric',
                                                hour12: false
                                            } ).format( new date(minhaPresenca.idTipoEventoNavigation.dataEvento))}</td>
                                            {/* <td>{minhaPresenca.idEventoNavigation.dataEvento}</td> */}
                                            <td>{minhaPresenca.idEventoNavigation.acessoLivre ? "Livre" : "Restrito"}</td>
                                            <td>{minhaPresenca.idSituacaoNavigation.descricao}</td>
                                            <td>{minhaPresenca.idEventoNavigation.idTipoEventoNavigation.tituloTipoEvento}</td>
                                            <td>{minhaPresenca.idTipoEventoNavigation.idInstituicaoNavigation.endereco}</td>
                                                                                       
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