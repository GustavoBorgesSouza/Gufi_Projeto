import React, { useState, useEffect } from 'react';
import axios from 'axios';

export default function TiposUsuarios() {
    //Não precismos de constructor com componentes funcionais, nem da função render(return direto)

    //estrutura do state criado através do Hook useState
    //const [ nomeState, funcaoAtualiza] = useState( valorInicial )
    //funcao atualiza é geralmente nomead como set + nomeState


    //Define o state listaTiposUsuarios, a função  setlistaTiposUsuarios que vai atualizar esse state
    const [ listaTiposUsuarios, setListaTiposUsuarios] = useState( [] );
    const [ titulo, setTitulo] = useState( "" );
    const [ isLoading, setIsLoading ] = useState( false );

    function buscarTiposUsuarios(){

        //faz a chamada para API usando axios
        axios("http://localhost:5000/api/tiposusuarios",{
            headers : {
                'Authorization' : 'Bearer' + localStorage.getItem('usuario-login')
            }
        })
        .then(resposta =>{
            if (resposta.status === 200) {
                setListaTiposUsuarios(resposta.data);
            }
        })
        .catch(erro => console.log(erro))
    };

    //estrutura do hook useEffect
    //useEffect( efeito, causa)
    //useEffect( {o que vai ser feito}, {o que será escutado})
    useEffect( buscarTiposUsuarios, []);

    function cadastrarTiposUsuarios(evento){
        evento.preventDefault();
        setIsLoading(true);

        axios.post("http://localhost:5000/api/tiposusuarios",{
            tituloTipoUsuario : titulo
        },{
            headers :{
                'Authorization' : 'Bearer' + localStorage.getItem('usuario-login')
            }
        })
        .then(resposta => {
            if (resposta.status === 201){
                console.log("tipo de usuario cadastrado");
                buscarTiposUsuarios();
                setTitulo("");
                setIsLoading(false);
            }
        })
        .catch( erro => console.log(erro), setInterval(() => {
            setIsLoading(false)
        }, 3000))
    };

    return (
        <div>
            <main>
                <section>
                    <h2>Tipos de Usuarios</h2>
                    <div>
                        <table style={{ borderCollapse: 'separate', borderSpacing: 30 }}>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Titulo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    listaTiposUsuarios.map((tipoUsuario) =>{
                                        <tr key={tipoUsuario.idTipoUsuario}>
                                            <td>{tipoUsuario.idTipoUsuario}</td>
                                            <td>{tipoUsuario.tituloTipoUsuarios}</td>
                                        </tr>
                                    })

                                }
                            </tbody>

                        </table>
                    </div>
                </section>
                    <form onSubmit={cadastrarTiposUsuarios}>
                        <div>
                            <input value={titulo} onChange={(campo) => setTitulo(campo.target.value)} type="text" placeholder="Titulo do tipo de usuario" />
                            

                            {
                                isLoading === false &&
                                <button type="submit">Cadastrar</button>
                            }

                            {
                                isLoading === true && 
                                <button disabled>Carregando</button>
                            }

                        </div>
                    </form>
                <section>

                </section>
            </main>
        </div>
    )
}